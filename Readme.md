<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml.cs](./CS/WpfDashboard_SqlDataSource/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/WpfDashboard_SqlDataSource/MainWindow.xaml.vb))
<!-- default file list end -->

# WPF Dashboard Viewer - How to bind a dashboard to a Microsoft SQL Server database file

This example demonstrates how to bind a dashboard to a Microsoft SQL Server database file (*.mdf).

The dashboard and its data source are created at runtime. A dashboard data source is a new instance of the [DashboardSqlDataSource](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardSqlDataSource) object containing the [SelectQuery](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Sql.SelectQuery) to retrieve data. The data source is added to the [Dashboard.DataSources](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.Dashboard.DataSources) collection. However, the data source connection parameters are not specified at this time. 

The dashboard is assigned to the [DashboardControl.DashboardSource](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWpf.DashboardControl.Dashboard) property within the _Window_Loaded_ event handler. 

Prior to data loading, the dashboard control fires the [DashboardControl.ConfigureDataConnection](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWpf.DashboardControl.ConfigureDataConnection) event. A connection string for the _NorthWind_ database file is assigned to the [CustomStringConnectionParameters.ConnectionString](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.ConnectionParameters.CustomStringConnectionParameters.ConnectionString) property accessible using the [e.ConnectionParameters](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs.ConnectionParameters) property. Note that the property returns an object of the base class, and you should cast it to the **CustomStringConnectionParameters** type.

Subsequently the data source is filled with data automatically, and the dashboard displays the data.

![](https://github.com/DevExpress-Examples/wpf-dashboard-how-to-bind-to-sql-database-file/blob/18.1.3%2B/images/WpfDashboard_SqlDataSource.png)

## Documentation

- [WPF Dashboard Viewer - Provide Data](https://docs.devexpress.com/Dashboard/119901/wpf-viewer/providing-data)

    Describes how to bind a dashboard displayed within the [DashboardControl](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWpf.DashboardControl) to data.
- [Visual Studio Designer - Provide Data](https://docs.devexpress.com/Dashboard/18295/wpf-viewer/create-dashboards-in-the-visual-studio-designer/provide-data)

    Describes how to bind a dashboard to data in [Visual Studio Designer](https://docs.devexpress.com/Dashboard/17519/wpf-viewer/create-dashboards-in-the-visual-studio-designer).
