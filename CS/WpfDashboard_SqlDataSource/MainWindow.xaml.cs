using DevExpress.DashboardCommon;
using DevExpress.DataAccess.Sql;
using System.Windows;

namespace WpfDashboard_SqlDataSource
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Dashboard dashboard = CreateDashboard();
            dashboardControl1.Dashboard = dashboard;
        }
        private IDashboardDataSource CreateSqlDataSource()
        {
            DashboardSqlDataSource sqlDataSource =
                new DashboardSqlDataSource("MySqlDataSource", "WpfDashboard_SqlDataSource.Properties.Settings.nwindConnectionString");
            SelectQuery selectQuery = SelectQueryFluentBuilder
                .AddTable("SalesPerson")
                .SelectColumns("CategoryName", "SalesPerson", "OrderDate", "ExtendedPrice")
                .Build("MyQuery");
            sqlDataSource.Queries.Add(selectQuery);
            sqlDataSource.Fill();
            return sqlDataSource;
        }
        private Dashboard CreateDashboard()
        {
            Dashboard dashBoard = new Dashboard();

            IDashboardDataSource sqlDataSource = CreateSqlDataSource();
            dashBoard.DataSources.Add(sqlDataSource);

            ChartDashboardItem chart = new ChartDashboardItem();
            chart.DataSource = sqlDataSource; chart.DataMember = "MyQuery";
            chart.Arguments.Add(new Dimension("OrderDate", DateTimeGroupInterval.MonthYear));
            chart.Panes.Add(new ChartPane());
            SimpleSeries salesAmountSeries = new SimpleSeries(SimpleSeriesType.SplineArea);
            salesAmountSeries.Value = new Measure("ExtendedPrice");
            chart.Panes[0].Series.Add(salesAmountSeries);

            GridDashboardItem grid = new GridDashboardItem();
            grid.DataSource = sqlDataSource;
            grid.DataMember = "MyQuery";
            grid.Columns.Add(new GridDimensionColumn(new Dimension("SalesPerson")));
            grid.Columns.Add(new GridMeasureColumn(new Measure("ExtendedPrice")));

            dashBoard.Items.AddRange(chart, grid);

            return dashBoard;
        }


    }
}
