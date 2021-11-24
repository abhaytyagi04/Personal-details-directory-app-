using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace personal_Data
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loadgrid();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-JN28RGVD\SQLEXPRESS;Initial Catalog=Personal_details;Integrated Security=True");
       
        public void clearData()
        {
            Name_txt.Clear();
            Gender_txt.Clear();
            Age_txt.Clear();
            City_txt.Clear();
            Search_txt.Clear();
        }
            
        public void Loadgrid()
        {
            SqlCommand cmd = new SqlCommand("select * from personal_detailss", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            Datagrid.ItemsSource = dt.DefaultView;
        }
       //insert button 
       public bool isvalid()
        {
            if (Name_txt.Text == string.Empty)
            {
                MessageBox.Show("Name is required","Failed" , MessageBoxButton.OK , MessageBoxImage.Error);
                return false; 
            }
            if (Age_txt.Text == string.Empty)
            {
                MessageBox.Show("Age is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Gender_txt.Text == string.Empty)
            {
                MessageBox.Show("Gender is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Age_txt.Text == string.Empty)
            {
                MessageBox.Show("Age is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (City_txt.Text == string.Empty)
            {
                MessageBox.Show("city is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true; 
        }
         private void Button_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                if (isvalid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO personal_detailss VALUES (@Name, @Age, @Gender, @city)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", Name_txt.Text);
                    cmd.Parameters.AddWithValue("@Gender", Gender_txt.Text);
                    cmd.Parameters.AddWithValue("@Age", Age_txt.Text);
                    cmd.Parameters.AddWithValue("@city", City_txt.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Loadgrid();
                    MessageBox.Show("Succesfully created data", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();
                }   
                
            }
            catch (SqlException ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void clear_btn_Click(object sender, RoutedEventArgs e)
        {

            clearData();
        }
        //update Button 
        private void Update_btn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update personal_detailss set Name = '" + Name_txt.Text + "' ,Age = '" + Age_txt.Text + "' , Gender = '" + Gender_txt.Text + "' ,City = '" + City_txt.Text + "'Where ID = '" + Search_txt.Text + "'", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been updated", "Updated ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                clearData();
                Loadgrid();
            }
        }

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from personal_detailss Where ID = " + Search_txt.Text + " ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                con.Close();
                clearData();
                Loadgrid();
               
            } 
            catch(SqlException ex)
            {
                MessageBox.Show("Not deleted" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
