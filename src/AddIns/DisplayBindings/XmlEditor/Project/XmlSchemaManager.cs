//
// SharpDevelop Xml Editor
//
// Copyright (C) 2005 Matthew Ward
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
// Matthew Ward (mrward@users.sourceforge.net)

using ICSharpCode.Core;
using System;
using System.IO;
using System.Xml;

namespace ICSharpCode.XmlEditor
{
	/// <summary>
	/// Keeps track of all the schemas that the Xml Editor is aware
	/// of.
	/// </summary>
	public class XmlSchemaManager
	{
		static XmlSchemaCompletionDataCollection schemas = null;
		static XmlSchemaManager manager = null;
		
		public static event EventHandler UserSchemaAdded;
		
		public static event EventHandler UserSchemaRemoved;
		
		XmlSchemaManager()
		{
		}
	
		/// <summary>
		/// Gets the schemas that SharpDevelop knows about.
		/// </summary>
		public static XmlSchemaCompletionDataCollection SchemaCompletionDataItems {
			get {
				if (schemas == null) {
					schemas = new XmlSchemaCompletionDataCollection();
					manager = new XmlSchemaManager();
					manager.ReadSchemas();
				}

				return schemas;
			}
		}
		
		/// <summary>
		/// Gets the schema completion data that is associated with the
		/// specified file extension.
		/// </summary>
		public static XmlSchemaCompletionData GetSchemaCompletionData(string extension)
		{
			XmlSchemaCompletionData data = null;
			
			XmlSchemaAssociation association = XmlEditorAddInOptions.GetSchemaAssociation(extension);
			if (association != null) {
				if (association.NamespaceUri.Length > 0) {
					data = SchemaCompletionDataItems[association.NamespaceUri];
				}
			}
			return data;
		}
		
		/// <summary>
		/// Gets the namespace prefix that is associated with the
		/// specified file extension.
		/// </summary>
		public static string GetNamespacePrefix(string extension)
		{
			string prefix = String.Empty;
			
			XmlSchemaAssociation association = XmlEditorAddInOptions.GetSchemaAssociation(extension);
			if (association != null) {
				prefix = association.NamespacePrefix;
			}
			
			return prefix;
		}
		
		/// <summary>
		/// Removes the schema with the specified namespace from the
		/// user schemas folder and removes the completion data.
		/// </summary>
		public static void RemoveUserSchema(string namespaceUri)
		{
			XmlSchemaCompletionData schemaData = SchemaCompletionDataItems[namespaceUri];
			if (schemaData != null) {
				if (File.Exists(schemaData.FileName)) {
					File.Delete(schemaData.FileName);
				}
				SchemaCompletionDataItems.Remove(schemaData);
				OnUserSchemaRemoved();
			}
		}
		
		/// <summary>
		/// Adds the schema to the user schemas folder and makes the
		/// schema available to the xml editor.
		/// </summary>
		public static void AddUserSchema(XmlSchemaCompletionData schemaData)
		{
			if (SchemaCompletionDataItems[schemaData.NamespaceUri] == null) {

				if (!Directory.Exists(UserSchemaFolder)) {
					Directory.CreateDirectory(UserSchemaFolder);
				}			
	
				string fileName = Path.GetFileName(schemaData.FileName);
				string destinationFileName = Path.Combine(UserSchemaFolder, fileName);
				File.Copy(schemaData.FileName, destinationFileName);
				schemaData.FileName = destinationFileName;
				SchemaCompletionDataItems.Add(schemaData);
				OnUserSchemaAdded();
			} else {
				Console.WriteLine(String.Concat("Trying to add a schema that already exists.  Namespace=", schemaData.NamespaceUri));
			}
		}		
		
		/// <summary>
		/// Reads the system and user added schemas.
		/// </summary>
		void ReadSchemas()
		{
			// MSBuild schemas are in framework directory:
			ReadSchemas(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), true);
			ReadSchemas(SchemaFolder, true);
			ReadSchemas(UserSchemaFolder, false);
		}
		
		/// <summary>
		/// Reads all .xsd files in the specified folder.
		/// </summary>
		void ReadSchemas(string folder, bool readOnly)
		{
			if (Directory.Exists(folder)) {
				foreach (string fileName in Directory.GetFiles(folder, "*.xsd")) {
					ReadSchema(fileName, readOnly);
				}
			}
		}
		
		/// <summary>
		/// Reads an individual schema and adds it to the collection.
		/// </summary>
		/// <remarks>
		/// If the schema namespace exists in the collection it is not added.
		/// </remarks>
		void ReadSchema(string fileName, bool readOnly)
		{
			try {
				XmlSchemaCompletionData data = new XmlSchemaCompletionData(fileName);
				if (data.NamespaceUri != null) {
					if (schemas[data.NamespaceUri] == null) {
						data.ReadOnly = readOnly;
						schemas.Add(data);
					} else {
						// Namespace already exists.
						Console.WriteLine(String.Concat("Ignoring duplicate schema namespace ", data.NamespaceUri));
					} 
				} else {
					// Namespace is null.
					Console.WriteLine(String.Concat("Ignoring schema with no namespace ", data.FileName));
				}
			} catch (Exception ex) {
				Console.WriteLine(String.Concat("Unable to read schema '", fileName, "'. ", ex.Message));
			}
		}
		
		/// <summary>
		/// Gets the folder where the schemas for all users on the
		/// local machine are stored.
		/// </summary>
		static string SchemaFolder {
			get {
				return Path.Combine(PropertyService.DataDirectory, "schemas");
			}
		}
		
		/// <summary>
		/// Gets the folder where schemas are stored for an individual user.
		/// </summary>
		static string UserSchemaFolder {
			get {
				return Path.Combine(PropertyService.ConfigDirectory, "schemas");				
			}
		}
		
		/// <summary>
		/// Should really pass schema info with the event.
		/// </summary>
		static void OnUserSchemaAdded()
		{
			if (UserSchemaAdded != null) {
				UserSchemaAdded(manager, new EventArgs());
			}
		}
		
		/// <summary>
		/// Should really pass schema info with the event.
		/// </summary>
		static void OnUserSchemaRemoved()
		{
			if (UserSchemaRemoved != null) {
				UserSchemaRemoved(manager, new EventArgs());
			}
		}
	}
}
