Class MainWindow

    Private ClipRctGeo As New EllipseGeometry()
    Private ClipPath As New Path()
    Private ScaleTr As New ScaleTransform()

    Private Sub MainWindow_Initialized(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Initialized
        'ClipRctGeo.Rect = New Rect(0, 0, 80, 80)
        ClipRctGeo.RadiusX = 40
        ClipRctGeo.RadiusY = 40
        ClipRctGeo.Center = New Point(40, 40)

        ClipPath.Stroke = Brushes.Gainsboro
        ClipPath.StrokeThickness = 2
        ClipPath.Data = ClipRctGeo

        ClipGrid.Children.Add(ClipPath)
        ClipGrid.Clip = ClipRctGeo

        ScaleTr.ScaleX = 2
        ScaleTr.ScaleY = 2

        ClipGrid.RenderTransform = ScaleTr
        ClipGrid.Visibility = Windows.Visibility.Hidden
    End Sub

    Private Sub MoveMagnifier(ByVal e As System.Windows.Input.MouseEventArgs)
        Dim mouseX As Double = e.GetPosition(ClipGrid).X
        Dim mouseY As Double = e.GetPosition(ClipGrid).Y

        ScaleTr.CenterX = mouseX
        ScaleTr.CenterY = mouseY

        ' Set the location and dimensions of the
        ' clipping rectangle.

        'ClipRctGeo.Rect = New Rect((mouseX - 40), (mouseY - 40), 80, 80)
        ClipRctGeo.Center = New Point(mouseX, mouseY)

    End Sub

    Private Sub ClipGrid_MouseLeave(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles ClipGrid.MouseLeave
        ' Hide the canvas when the pointer is beyond
        ' the region of interest.
        ClipGrid.Visibility = Windows.Visibility.Hidden
    End Sub

    Private Sub ClipGrid_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles ClipGrid.MouseMove
        ' Move magnifying region.
        MoveMagnifier(e)
    End Sub

    Private Sub MainGrid_MouseEnter(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles MainGrid.MouseEnter
        ' Set magnifying location.
        MoveMagnifier(e)

        If ClipGrid.Visibility = Windows.Visibility.Hidden Then
            ClipGrid.Visibility = Windows.Visibility.Visible
        End If
    End Sub
End Class