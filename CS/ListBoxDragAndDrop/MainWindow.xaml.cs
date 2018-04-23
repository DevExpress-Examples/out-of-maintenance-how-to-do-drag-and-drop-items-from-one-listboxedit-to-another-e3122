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

namespace ListBoxDragAndDrop {
    public partial class MainWindow : Window {

        bool isDragStarted;

        public MainWindow() {
            InitializeComponent();

            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e) {

            listBoxEdit1.AllowDrop = true;
            listBoxEdit2.AllowDrop = true;

            listBoxEdit1.AddHandler(ListBoxEdit.MouseLeftButtonDownEvent,  new MouseButtonEventHandler(list_MouseLeftButtonDown), true);
            listBoxEdit1.PreviewMouseMove += new MouseEventHandler(list_PreviewMouseMove);
            listBoxEdit1.DragOver += new DragEventHandler(list_DragOver);
            listBoxEdit1.Drop += new DragEventHandler(list_Drop);

            listBoxEdit2.AddHandler(ListBoxEdit.MouseLeftButtonDownEvent, new MouseButtonEventHandler(list_MouseLeftButtonDown), true);
            listBoxEdit2.PreviewMouseMove += new MouseEventHandler(list_PreviewMouseMove);
            listBoxEdit2.DragOver += new DragEventHandler(list_DragOver);
            listBoxEdit2.Drop += new DragEventHandler(list_Drop);

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

        void list_Drop(object sender, DragEventArgs e) {
            TestData item = (TestData)e.Data.GetData(typeof(TestData));
            ObservableCollection<TestData> senderItemsSource = (ObservableCollection<TestData>)((ListBoxEdit)sender).ItemsSource;

            if (!senderItemsSource.Contains(item) || IsCopyEffect(e)) {

                if (!IsCopyEffect(e)) {
                    ListBoxEdit dragSourceListBox = (ListBoxEdit)e.Data.GetData("dragSource");
                    ObservableCollection<TestData> dragSourceItemsSource = (ObservableCollection<TestData>)dragSourceListBox.ItemsSource;

                    dragSourceItemsSource.Remove(item);
                }

                TestData copyItem = new TestData() { Text = item.Text };
                senderItemsSource.Add(copyItem);
            }
        }

        void list_DragOver(object sender, DragEventArgs e) {
            TestData item = (TestData)e.Data.GetData(typeof(TestData));
            ObservableCollection<TestData> senderItemsSource = (ObservableCollection<TestData>)((ListBoxEdit)sender).ItemsSource;

            if (senderItemsSource.Contains(item)) {
                e.Effects = IsCopyEffect(e) ? DragDropEffects.Copy : DragDropEffects.None;
            } else {
                e.Effects = IsCopyEffect(e) ? DragDropEffects.Copy : DragDropEffects.Move;
            }

            e.Handled = true;
        }

        bool IsCopyEffect(DragEventArgs e) {
            return (e.KeyStates & DragDropKeyStates.ControlKey) == DragDropKeyStates.ControlKey;
        }

        void list_PreviewMouseMove(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                if (isDragStarted) {
                    ListBoxEdit listBoxEdit = (ListBoxEdit)sender;
                    TestData item = (TestData)listBoxEdit.SelectedItem;

                    if (item != null) {
                        DataObject data = CreateDataObject(item);
                        data.SetData("dragSource", listBoxEdit);
                        DragDrop.DoDragDrop(listBoxEdit, data, DragDropEffects.Move | DragDropEffects.Copy);
                        isDragStarted = false;
                    }
                }
            }
        }

        void list_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            ListBoxEdit listBoxEdit = (ListBoxEdit)sender;
            DependencyObject hittedObject = listBoxEdit.InputHitTest(e.GetPosition(listBoxEdit)) as DependencyObject;
            FrameworkElement hittedItem = LayoutHelper.FindParentObject<ListBoxEditItem>(hittedObject);

            if (hittedItem != null) {
                isDragStarted = true;
            }
        }

        private DataObject CreateDataObject(TestData item) {
            DataObject data = new DataObject();
            data.SetData(typeof(TestData), item);
            return data;
        }
    }

    public class TestData {
        public string Text {get; set;}
    }
}
