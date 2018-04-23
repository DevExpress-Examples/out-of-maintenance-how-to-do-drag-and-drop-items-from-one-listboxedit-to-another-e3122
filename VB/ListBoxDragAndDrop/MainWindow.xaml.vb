Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Grid

Namespace ListBoxDragAndDrop
    Partial Public Class MainWindow
        Inherits Window

        Private isDragStarted As Boolean

        Public Sub New()
            InitializeComponent()

            AddHandler Loaded, AddressOf MainWindow_Loaded
        End Sub

        Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)



'            #Region "ItemsSources initialization."
            listBoxEdit1.ItemsSource = New ObservableCollection(Of TestData)() From { _
                New TestData() With {.Text = "Item 1.1"}, _
                New TestData() With {.Text = "Item 1.2"}, _
                New TestData() With {.Text = "Item 1.3"}, _
                New TestData() With {.Text = "Item 1.4"} _
            }

            listBoxEdit2.ItemsSource = New ObservableCollection(Of TestData)() From { _
                New TestData() With {.Text = "Item 2.1"}, _
                New TestData() With {.Text = "Item 2.2"}, _
                New TestData() With {.Text = "Item 2.3"}, _
                New TestData() With {.Text = "Item 2.4"} _
            }
'            #End Region
        End Sub
    End Class



    Public Class TestData
        Public Property Text() As String
    End Class
End Namespace
