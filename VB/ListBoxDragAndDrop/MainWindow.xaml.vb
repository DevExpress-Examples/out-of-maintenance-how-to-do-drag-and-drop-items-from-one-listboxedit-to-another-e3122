Imports System.Windows
Imports System.Windows.Controls
Imports System.Collections.ObjectModel

Namespace ListBoxDragAndDrop

    Public Partial Class MainWindow
        Inherits Window

        Private isDragStarted As Boolean

        Public Sub New()
            Me.InitializeComponent()
            AddHandler Loaded, New RoutedEventHandler(AddressOf Me.MainWindow_Loaded)
        End Sub

        Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
'#Region "ItemsSources initialization."
            Me.listBoxEdit1.ItemsSource = New ObservableCollection(Of TestData)() From {New TestData() With {.Text = "Item 1.1"}, New TestData() With {.Text = "Item 1.2"}, New TestData() With {.Text = "Item 1.3"}, New TestData() With {.Text = "Item 1.4"}}
            Me.listBoxEdit2.ItemsSource = New ObservableCollection(Of TestData)() From {New TestData() With {.Text = "Item 2.1"}, New TestData() With {.Text = "Item 2.2"}, New TestData() With {.Text = "Item 2.3"}, New TestData() With {.Text = "Item 2.4"}}
'#End Region
        End Sub
    End Class

    Public Class TestData

        Public Property Text As String
    End Class
End Namespace
