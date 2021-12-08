Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports System.Windows

Namespace WpfDashboard_SqlDataSource

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim dashboard As Dashboard = CreateDashboard()
            Me.dashboardControl1.Dashboard = dashboard
        End Sub

        Private Function CreateSqlDataSource() As IDashboardDataSource
            Dim sqlDataSource As DashboardSqlDataSource = New DashboardSqlDataSource("MySqlDataSource")
            Dim selectQuery As SelectQuery = SelectQueryFluentBuilder.AddTable("SalesPerson").SelectColumns("CategoryName", "SalesPerson", "OrderDate", "ExtendedPrice").Build("MyQuery")
            sqlDataSource.Queries.Add(selectQuery)
            Return sqlDataSource
        End Function

        Private Function CreateDashboard() As Dashboard
            Dim dashBoard As Dashboard = New Dashboard()
            Dim sqlDataSource As IDashboardDataSource = CreateSqlDataSource()
            dashBoard.DataSources.Add(sqlDataSource)
            Dim chart As ChartDashboardItem = New ChartDashboardItem()
            chart.DataSource = sqlDataSource
            chart.DataMember = "MyQuery"
            chart.Arguments.Add(New Dimension("OrderDate", DateTimeGroupInterval.MonthYear))
            chart.Panes.Add(New ChartPane())
            Dim salesAmountSeries As SimpleSeries = New SimpleSeries(SimpleSeriesType.SplineArea)
            salesAmountSeries.Value = New Measure("ExtendedPrice")
            chart.Panes(0).Series.Add(salesAmountSeries)
            Dim grid As GridDashboardItem = New GridDashboardItem()
            grid.DataSource = sqlDataSource
            grid.DataMember = "MyQuery"
            grid.Columns.Add(New GridDimensionColumn(New Dimension("SalesPerson")))
            grid.Columns.Add(New GridMeasureColumn(New Measure("ExtendedPrice")))
            dashBoard.Items.AddRange(chart, grid)
            Return dashBoard
        End Function

        Private Sub dashboardControl1_ConfigureDataConnection(ByVal sender As Object, ByVal e As DashboardConfigureDataConnectionEventArgs)
            Dim parameters As CustomStringConnectionParameters = TryCast(e.ConnectionParameters, CustomStringConnectionParameters)
            If Equals(e.DataSourceName, "MySqlDataSource") Then parameters.ConnectionString = "XpoProvider=MSSqlServer;Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NWind.mdf;Integrated Security=True"
        End Sub
    End Class
End Namespace
