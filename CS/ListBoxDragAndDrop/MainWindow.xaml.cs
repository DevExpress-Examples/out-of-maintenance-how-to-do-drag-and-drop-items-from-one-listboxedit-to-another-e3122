using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid;

namespace ListBoxDragAndDrop {
    public partial class MainWindow : Window {

        bool isDragStarted;

        public MainWindow() {
            InitializeComponent();
       
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e) {

            

            #region ItemsSources initialization.
            listBoxEdit1.ItemsSource = new ObservableCollection<TestData>() { 
                new TestData(){ Text = "Item 1.1"}, 
                new TestData(){ Text = "Item 1.2"},
                new TestData(){ Text = "Item 1.3"},
                new TestData(){ Text = "Item 1.4"}
            };

            listBoxEdit2.ItemsSource = new ObservableCollection<TestData>() { 
                new TestData(){ Text = "Item 2.1"}, 
                new TestData(){ Text = "Item 2.2"},
                new TestData(){ Text = "Item 2.3"},
                new TestData(){ Text = "Item 2.4"}
            };
            #endregion
        }

       
    }

    public class TestData {
        public string Text {get; set;}
    }
}
