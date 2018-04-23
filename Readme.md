# How to do drag and drop items from one ListBoxEdit to another


<p>This example shows how to implement the drag-and-drop functionality for ListBoxEdit.</p>
<p>First of all, you need to set the <a href="http://documentation.devexpress.dev/#WPF/DevExpressXpfCoreDXFrameworkContentElement_AllowDroptopic"><u>AllowDrop</u></a> property to true, in order to let your editor to accept dropping. Next, you'll need implement four event handlers for the editor to manage the drag-and-drop process:</p>
<p>1. <strong>MouseLeftButtonDown</strong> event handler. Within this handler, it is necessary to find out if the click occurred on a certain item. If so, the <strong>isDragStarted</strong> flag is set to True, to allow all the following processing.</p>
<p>2. <strong>PreviewMouseMove</strong> event handler. If the <strong>isDragStarted</strong> flag is set to True, it then defines a dragged item and the dragging source object. Then, the <strong>DragDrop.DoDragDrop() </strong>method that initiates a drag-and-drop operation is invoked.</p>
<p>3. <strong>DragOver</strong> event handler. Defines the behavior of a drag-and-drop operation: if the event's source object can accept a dragged object, the <strong>e.Effects</strong> property is set to the appropriate <strong>DragDropEffect</strong> value. Otherwise, it is set to the <strong>DragDropEffects.None</strong> value.</p>
<p>4. <strong>Drop</strong> event handler. Handles accepting the dragged item by dragging the destination object. Note, that it is necessary to create a clone of the copying item to avoid collisions. Do not add a dragged item to the items collection itself, until you are sure that this is appropriate for your task.<br /><br /></p>
<p><strong>Update:</strong><br /><br /><strong>Starting from the 12.1 version, we have the built-in ListBoxDragDropManager. </strong></p>
<p><strong>We also have an example that shows how to reorder elements in the ListBoxEdit: <a href="https://www.devexpress.com/Support/Center/p/E4598">E4598: ListBoxDragDropManager - How to reorder items</a></strong></p>

<br/>


