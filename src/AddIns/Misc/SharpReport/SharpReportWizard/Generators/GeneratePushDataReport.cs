//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

using System;

using ICSharpCode.Core;
	
using SharpReportCore;

/// <summary>
/// This class is used to generate PushDataReports
/// (Reports, that are feed with an DataSet etc)
/// </summary>
/// <remarks>
/// 	created by - Forstmeier Peter
/// 	created on - 08.09.2005 10:10:19
/// </remarks>
	
namespace ReportGenerator {
	public class GeneratePushDataReport : AbstractReportGenerator {
		
		/// <summary>
		/// Default constructor - initializes all fields to default values
		/// </summary>
		public GeneratePushDataReport(Properties customizer,
		                              ReportModel reportModel):base(customizer,reportModel){
			
			if (customizer == null) {
				throw new ArgumentException("customizer");
			}
			if (reportModel == null) {
				throw new ArgumentException("reportModel");
			}
			if (base.ReportModel.ReportSettings.DataModel != GlobalEnums.PushPullModel.PushData) {
				throw new ArgumentException ("Wrong DataModel in GeneratePushReport");
			}
			//we can't use the customizer here
			base.ReportItemCollection.Clear();
			base.ReportItemCollection.AddRange(base.ReportGenerator.ReportItemCollection);
		}
		
		public override void GenerateReport() {
			
			base.ReportModel.ReportSettings.ReportType = GlobalEnums.ReportType.DataReport;
			base.ReportModel.ReportSettings.DataModel = GlobalEnums.PushPullModel.PushData;
			
			
			base.ReportModel.ReportSettings.AvailableFieldsCollection = base.ReportGenerator.ColumnCollection;
			base.GenerateReport();
			base.HeaderColumnsFromReportItems (base.ReportModel.PageHeader);
			base.BuildDataSection (base.ReportModel.DetailSection);
			using (TableLayout layout = new TableLayout(base.ReportModel)){
				layout.BuildLayout();
			}
			base.AdjustAllNames();
			
		}
	}
}
