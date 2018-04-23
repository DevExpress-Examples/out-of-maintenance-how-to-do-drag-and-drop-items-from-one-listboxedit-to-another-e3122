Imports Microsoft.VisualBasic
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

Namespace ListBoxDragAndDrop
	Partial Public Class MainWindow
		Inherits Window

		Private isDragStarted As Boolean

		Public Sub New()
			InitializeComponent()

			AddHandler Loaded, AddressOf MainWindow_Loaded
		End Sub

		Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)

			listBoxEdit1.AllowDrop = True
			listBoxEdit2.AllowDrop = True

			listBoxEdit1.AddHandler(ListBoxEdit.MouseLeftButtonDownEvent, New MouseButtonEventHandler(AddressOf list_MouseLeftButtonDown), True)
			AddHandler listBoxEdit1.PreviewMouseMove, AddressOf list_PreviewMouseMove
			AddHandler listBoxEdit1.DragOver, AddressOf list_DragOver
			AddHandler listBoxEdit1.Drop, AddressOf list_Drop

			listBoxEdit2.AddHandler(ListBoxEdit.MouseLeftButtonDownEvent, New MouseButtonEventHandler(AddressOf list_MouseLeftButtonDown), True)
			AddHandler listBoxEdit2.PreviewMouseMove, AddressOf list_PreviewMouseMove
			AddHandler listBoxEdit2.DragOver, AddressOf list_DragOver
			AddHandler listBoxEdit2.Drop, AddressOf list_Drop

'			#Region "ItemsSources initialization."
			listBoxEdit1.ItemsSource = New ObservableCollection(Of TestData) (New TestData() {New TestData() With {.Text = "Item 1.1"}, New TestData() With {.Text = "Item 1.2"}, New TestData() With {.Text = "Item 1.3"}, New TestData() With {.Text = "Item 1.4"}})

			listBoxEdit2.ItemsSource = New ObservableCollection(Of TestData) (New TestData() {New TestData() With {.Text = "Item 2.1"}, New TestData() With {.Text = "Item 2.2"}, New TestData() With {.Text = "Item 2.3"}, New TestData() With {.Text = "Item 2.4"}})
'			#End Region
		End Sub

		Private Sub list_Drop(ByVal sender As Object, ByVal e As DragEventArgs)
			Dim item As TestData = CType(e.Data.GetData(GetType(TestData)), TestData)
			Dim senderItemsSource As ObservableCollection(Of TestData) = CType((CType(sender, ListBoxEdit)).ItemsSource, ObservableCollection(Of TestData))

			If (Not senderItemsSource.Contains(item)) OrElse IsCopyEffect(e) Then

				If (Not IsCopyEffect(e)) Then
					Dim dragSourceListBox As ListBoxEdit = CType(e.Data.GetData("dragSource"), ListBoxEdit)
					Dim dragSourceItemsSource As ObservableCollection(Of TestData) = CType(dragSourceListBox.ItemsSource, ObservableCollection(Of TestData))

					dragSourceItemsSource.Remove(item)
				End If

				Dim copyItem As New TestData() With {.Text = item.Text}
				senderItemsSource.Add(copyItem)
			End If
		End Sub

		Private Sub list_DragOver(ByVal sender As Object, ByVal e As DragEventArgs)
			Dim item As TestData = CType(e.Data.GetData(GetType(TestData)), TestData)
			Dim senderItemsSource As ObservableCollection(Of TestData) = CType((CType(sender, ListBoxEdit)).ItemsSource, ObservableCollection(Of TestData))

			If senderItemsSource.Contains(item) Then
				e.Effects = If(IsCopyEffect(e), DragDropEffects.Copy, DragDropEffects.None)
			Else
				e.Effects = If(IsCopyEffect(e), DragDropEffects.Copy, DragDropEffects.Move)
			End If

			e.Handled = True
		End Sub

		Private Function IsCopyEffect(ByVal e As DragEventArgs) As Boolean
			Return (e.KeyStates And DragDropKeyStates.ControlKey) = DragDropKeyStates.ControlKey
		End Function

		Private Sub list_PreviewMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
			If e.LeftButton = MouseButtonState.Pressed Then
				If isDragStarted Then
					Dim listBoxEdit As ListBoxEdit = CType(sender, ListBoxEdit)
					Dim item As TestData = CType(listBoxEdit.SelectedItem, TestData)

					If item IsNot Nothing Then
						Dim data As DataObject = CreateDataObject(item)
						data.SetData("dragSource", listBoxEdit)
						DragDrop.DoDragDrop(listBoxEdit, data, DragDropEffects.Move Or DragDropEffects.Copy)
						isDragStarted = False
					End If
				End If
			End If
		End Sub

		Private Sub list_MouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
			Dim listBoxEdit As ListBoxEdit = CType(sender, ListBoxEdit)
			Dim hittedObject As DependencyObject = TryCast(listBoxEdit.InputHitTest(e.GetPosition(listBoxEdit)), DependencyObject)
			Dim hittedItem As FrameworkElement = LayoutHelper.FindParentObject(Of ListBoxEditItem)(hittedObject)

			If hittedItem IsNot Nothing Then
				isDragStarted = True
			End If
		End Sub

		Private Function CreateDataObject(ByVal item As TestData) As DataObject
			Dim data As New DataObject()
			data.SetData(GetType(TestData), item)
			Return data
		End Function
	End Class

	Public Class TestData
		Private privateText As String
		Public Property Text() As String
			Get
				Return privateText
			End Get
			Set(ByVal value As String)
				privateText = value
			End Set
		End Property
	End Class
End Namespace
