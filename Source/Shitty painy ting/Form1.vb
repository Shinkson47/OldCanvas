Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Public Class Form1
    Private mouse As Boolean
    Public Colour As Color
    Public StoredColourNumber As Integer = 0
    Public DrawingX As Integer = Windows.Forms.Cursor.Position.X
    Public DrawingY As Integer = Windows.Forms.Cursor.Position.Y

    Private Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Select Case e.KeyChar
            Case " "
                ToggleToolStripMenuItem_Click(Me, EventArgs.Empty)
                Exit Sub
            Case "q"
                prev = vbNull
                newx = New Point
                used = False

        End Select


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Height = Screen.PrimaryScreen.Bounds.Height
        Me.Width = Screen.PrimaryScreen.Bounds.Width
        Me.Left = 0
        Me.Top = 0
        ComboShape.SelectedIndex = 1
        Me.KeyPreview = True
        Me.Text = "Paint++ - " & Me.Height & " X " & Me.Width
        Timer1.Start()
        '------------------------------------
        colourupdate()
        pslx.Text = PenSizeX.Value.ToString
        psly.Text = PenSizeY.Value.ToString
        '------------------------------------
    End Sub

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        mouse = True
    End Sub


    Private Sub Form1_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        mouse = False
    End Sub


    Private Sub PenSizeX_Scroll(sender As Object, e As EventArgs) Handles PenSizeX.Scroll
        If AR.Checked Then
            PenSizeY.Value = PenSizeX.Value
        End If
        pslx.Text = PenSizeX.Value.ToString
        psly.Text = PenSizeY.Value.ToString
    End Sub

    Private Sub PenSizeY_Scroll(sender As Object, e As EventArgs) Handles PenSizeY.Scroll
        If AR.Checked Then
            PenSizeX.Value = PenSizeY.Value
        End If
        pslx.Text = PenSizeX.Value.ToString
        psly.Text = PenSizeY.Value.ToString
    End Sub

    Private Sub colourupdate()


        TBBlue.BackColor = Color.FromArgb(TBRed.Value, TBGreen.Value, TBBlue.Value)
        TBRed.BackColor = Color.FromArgb(TBRed.Value, TBGreen.Value, TBBlue.Value)
        TBGreen.BackColor = Color.FromArgb(TBRed.Value, TBGreen.Value, TBBlue.Value)
        Opacity.BackColor = Color.FromArgb(TBRed.Value, TBGreen.Value, TBBlue.Value)


    End Sub

    Private Sub TBRed_Scroll(sender As Object, e As EventArgs) Handles TBRed.Scroll
        colourupdate()
    End Sub

    Private Sub TBGreen_Scroll(sender As Object, e As EventArgs) Handles TBGreen.Scroll
        colourupdate()
    End Sub

    Private Sub TBBlue_Scroll(sender As Object, e As EventArgs) Handles TBBlue.Scroll
        colourupdate()
    End Sub

    Private Sub Randomise_CheckedChanged(sender As Object, e As EventArgs) Handles Randomise.CheckedChanged
        If Randomise.Checked Then
            Label1.Text = "Max / Min X"
            Label6.Text = "Max / Min X"
            Maxx.Show()
            Maxy.Show()
            lblMaxx.Show()
            lblMaxy.Show()
        Else
            Label1.Text = "PenX"
            Label6.Text = "PenY"
            Maxx.Hide()
            Maxy.Hide()
            lblMaxx.Hide()
            lblMaxy.Hide()
        End If
    End Sub

    Private Sub Maxy_Scroll(sender As Object, e As EventArgs) Handles Maxy.Scroll
        If AR.Checked Then
            Maxx.Value = Maxy.Value
        End If
        lblMaxy.Text = CStr(Maxy.Value)
        lblMaxx.Text = CStr(Maxx.Value)
    End Sub

    Private Sub Maxx_Scroll(sender As Object, e As EventArgs) Handles Maxx.Scroll
        lblMaxy.Text = CStr(Maxy.Value)
        lblMaxx.Text = CStr(Maxx.Value)
        If AR.Checked Then
            Maxy.Value = Maxx.Value
        End If
    End Sub


    Private Sub Form1_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        If e.Delta > 0 Then
            If Opacity.Value >= 250 Then
                Return
            End If
            Opacity.Value += 5
        Else
            If Opacity.Value <= 5 Then
                Return
            End If
            Opacity.Value += -5
        End If

    End Sub


    Private Sub DeleteAllThatShit_Click(sender As Object, e As EventArgs) Handles DeleteAllThatShit.Click
        Dim graphics = Me.CreateGraphics
        graphics.Clear(Color.White)
    End Sub

    Private Sub ToggleToolStripMenuItem_Click(sender As Object, e As EventArgs)
        GroupBox1.Visible = Not GroupBox1.Visible
        GroupBox2.Visible = Not GroupBox2.Visible
    End Sub

    Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim graphics = Me.CreateGraphics
        graphics.Clear(Color.FromArgb(TBRed.Value, TBGreen.Value, TBBlue.Value))
    End Sub
    Public Sub FillPathEllipse(ByVal e As PaintEventArgs)

        ' Create solid brush.
        Dim redBrush As New SolidBrush(Color.Red)

        ' Create graphics path object and add ellipse.
        Dim graphPath As New GraphicsPath
        graphPath.AddEllipse(0, 0, 200, 100)

        ' Fill graphics path to screen.
        e.Graphics.FillPath(redBrush, graphPath)
    End Sub

    Dim prev = vbNull
    Dim newx As Point
    Dim used As Boolean = False

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim tickrule = 0
        Dim graphics As Graphics = Me.CreateGraphics
        lblRedValue.Text = TBRed.Value
        lblGreenValue.Text = TBGreen.Value
        lblBlueValue.Text = TBBlue.Value
        lblOpacity.Text = Convert.ToInt32((Opacity.Value / 255) * 100) & " %"
        Console.WriteLine(CStr(mouse) & " " & Cursor.Position.X & " " & Cursor.Position.Y & " " & TBRed.Value & " " & TBGreen.Value & " " & TBBlue.Value & " " & Opacity.Value)
        Dim mybrush As Brush = New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(Opacity.Value, TBRed.Value, TBGreen.Value, TBBlue.Value))



        If mouse Then
            '--------------------------------------------------------------------------------------------------------------------------
            If StoredColour1.BackColor = System.Drawing.Color.White Then
                StoredColour1.BackColor = TBBlue.BackColor
            ElseIf StoredColour1.BackColor <> System.Drawing.Color.White And StoredColour2.BackColor = System.Drawing.Color.White Then
                StoredColour2.BackColor = TBBlue.BackColor
            ElseIf StoredColour2.BackColor <> System.Drawing.Color.White And StoredColour3.BackColor = System.Drawing.Color.White Then
                StoredColour3.BackColor = TBBlue.BackColor
            ElseIf StoredColour3.BackColor <> System.Drawing.Color.White And StoredColour4.BackColor = System.Drawing.Color.White Then
                StoredColour4.BackColor = TBBlue.BackColor
            ElseIf StoredColour4.BackColor <> System.Drawing.Color.White And StoredColour5.BackColor = System.Drawing.Color.White Then
                StoredColour5.BackColor = TBBlue.BackColor
            ElseIf StoredColour5.BackColor <> System.Drawing.Color.White And StoredColour6.BackColor = System.Drawing.Color.White Then
                StoredColour6.BackColor = TBBlue.BackColor
            ElseIf StoredColour6.BackColor <> System.Drawing.Color.White And StoredColour7.BackColor = System.Drawing.Color.White Then
                StoredColour7.BackColor = TBBlue.BackColor
            ElseIf StoredColour7.BackColor <> System.Drawing.Color.White And StoredColour8.BackColor = System.Drawing.Color.White Then
                StoredColour8.BackColor = TBBlue.BackColor
            End If

            If StoredColour2.BackColor = StoredColour1.BackColor Then
                StoredColour2.BackColor = System.Drawing.Color.White
            End If
            If StoredColour3.BackColor = StoredColour1.BackColor Or StoredColour3.BackColor = StoredColour2.BackColor Then
                StoredColour3.BackColor = System.Drawing.Color.White
            End If
            If StoredColour4.BackColor = StoredColour1.BackColor Or StoredColour4.BackColor = StoredColour2.BackColor Or StoredColour4.BackColor = StoredColour3.BackColor Then
                StoredColour4.BackColor = System.Drawing.Color.White
            End If
            If StoredColour5.BackColor = StoredColour1.BackColor Or StoredColour5.BackColor = StoredColour2.BackColor Or StoredColour5.BackColor = StoredColour3.BackColor Or StoredColour5.BackColor = StoredColour4.BackColor Then
                StoredColour5.BackColor = System.Drawing.Color.White
            End If
            If StoredColour6.BackColor = StoredColour1.BackColor Or StoredColour6.BackColor = StoredColour2.BackColor Or StoredColour6.BackColor = StoredColour3.BackColor Or StoredColour6.BackColor = StoredColour4.BackColor Or StoredColour6.BackColor = StoredColour5.BackColor Then
                StoredColour6.BackColor = System.Drawing.Color.White
            End If
            If StoredColour7.BackColor = StoredColour1.BackColor Or StoredColour7.BackColor = StoredColour2.BackColor Or StoredColour7.BackColor = StoredColour3.BackColor Or StoredColour7.BackColor = StoredColour4.BackColor Or StoredColour7.BackColor = StoredColour5.BackColor Or StoredColour7.BackColor = StoredColour6.BackColor Then
                StoredColour7.BackColor = System.Drawing.Color.White
            End If
            If StoredColour8.BackColor = StoredColour1.BackColor Or StoredColour8.BackColor = StoredColour2.BackColor Or StoredColour8.BackColor = StoredColour3.BackColor Or StoredColour8.BackColor = StoredColour4.BackColor Or StoredColour8.BackColor = StoredColour5.BackColor Or StoredColour8.BackColor = StoredColour6.BackColor Or StoredColour8.BackColor = StoredColour7.BackColor Then
                StoredColour8.BackColor = System.Drawing.Color.White
            End If
            '-------------------------------------------------------------------------------------------------------------------------
            Select Case ComboShape.SelectedItem
                Case "Cluster"
                    Dim cluster_density As Integer = TrackBarCluster.Value

                    For i = 1 To 5
                        Dim x As Integer = Rnd() * cluster_density
                        Dim y As Integer = Rnd() * cluster_density

                        graphics.FillEllipse(mybrush, New Rectangle(Windows.Forms.Cursor.Position.X - Me.Left - 30 - x, Windows.Forms.Cursor.Position.Y - Me.Top - 30 - y, Int((Maxx.Value - PenSizeX.Value + 1) * Rnd() + PenSizeX.Value), Int((Maxy.Value - PenSizeY.Value + 1) * Rnd() + PenSizeY.Value)))

                    Next
                Case "Big Circle"
                    graphics.FillEllipse(mybrush, New Rectangle(Windows.Forms.Cursor.Position.X - Me.Left - 30, Windows.Forms.Cursor.Position.Y - Me.Top - 30, (Int((Maxx.Value - PenSizeX.Value + 1) * Rnd() + PenSizeX.Value)) * 3, (Int((Maxy.Value - PenSizeY.Value + 1) * Rnd() + PenSizeY.Value)) * 3))
                Case "Rubber"
                    TBBlue.BackColor = System.Drawing.Color.White
                    TBRed.BackColor = System.Drawing.Color.White
                    TBGreen.BackColor = System.Drawing.Color.White
                    Opacity.BackColor = System.Drawing.Color.White

                    TBRed.Value = 255
                    TBGreen.Value = 255
                    TBBlue.Value = 255
                    Opacity.Value = 255
                    graphics.FillEllipse(mybrush, New Rectangle(Windows.Forms.Cursor.Position.X - Me.Left - 30, Windows.Forms.Cursor.Position.Y - Me.Top - 30, Int((Maxx.Value - PenSizeX.Value + 1) * Rnd() + PenSizeX.Value), Int((Maxy.Value - PenSizeY.Value + 1) * Rnd() + PenSizeY.Value)))
                Case "Line Revised"

                    Dim currentX As Integer = Windows.Forms.Cursor.Position.X
                    Dim currentY As Integer = Windows.Forms.Cursor.Position.Y


                    Do Until DrawingX = currentX And DrawingY = currentY

                        If DrawingX > currentX Then
                            DrawingX = DrawingX - 1
                        End If
                        If DrawingX < currentX Then
                            DrawingX = DrawingX + 1
                        End If

                        If DrawingY > currentY Then
                            DrawingY = DrawingY - 1
                        End If
                        If DrawingY < currentY Then
                            DrawingY = DrawingY + 1
                        End If

                        graphics.FillEllipse(mybrush, New Rectangle(DrawingX - Me.Left - (PenSizeX.Value / 2) - 10, DrawingY - Me.Top - 30 - (PenSizeY.Value / 2), PenSizeX.Value, PenSizeY.Value))

                    Loop


                Case "Elipse"
                    If Randomise.Checked Then
                        graphics.FillEllipse(mybrush, New Rectangle(Windows.Forms.Cursor.Position.X - Me.Left - 30, Windows.Forms.Cursor.Position.Y - Me.Top - 30, Int((Maxx.Value - PenSizeX.Value + 1) * Rnd() + PenSizeX.Value), Int((Maxy.Value - PenSizeY.Value + 1) * Rnd() + PenSizeY.Value)))
                        Return
                    End If

                    graphics.FillEllipse(mybrush, New Rectangle(Windows.Forms.Cursor.Position.X - Me.Left - (PenSizeX.Value / 2) - 10, Windows.Forms.Cursor.Position.Y - Me.Top - 30 - (PenSizeY.Value / 2), PenSizeX.Value, PenSizeY.Value))
                Case "Rectangle"
                    If Randomise.Checked Then
                        graphics.FillRectangle(mybrush, New Rectangle(Windows.Forms.Cursor.Position.X - Me.Left - 30, Windows.Forms.Cursor.Position.Y - Me.Top - 30, Int((Maxx.Value - PenSizeX.Value + 1) * Rnd() + PenSizeX.Value), Int((Maxy.Value - PenSizeY.Value + 1) * Rnd() + PenSizeY.Value)))
                        Return
                    End If
                    graphics.FillRectangle(mybrush, New Rectangle(Windows.Forms.Cursor.Position.X - Me.Left - (PenSizeX.Value / 2) - 10, Windows.Forms.Cursor.Position.Y - Me.Top - 30 - (PenSizeY.Value / 2), PenSizeX.Value, PenSizeY.Value))

                Case "Line"
                    Try
                        If prev = vbNull Then
                            prev = New Point(Windows.Forms.Cursor.Position.X - Me.Left - (PenSizeX.Value / 2) - 10, Windows.Forms.Cursor.Position.Y - Me.Top - 30 - (PenSizeY.Value / 2))
                        Else
                            prev = newx
                        End If
                    Catch
                        prev = newx
                    End Try
                    newx = New Point(Windows.Forms.Cursor.Position.X - Me.Left - (PenSizeX.Value / 2) - 10, Windows.Forms.Cursor.Position.Y - Me.Top - 30 - (PenSizeY.Value / 2))

                    graphics.DrawLine(New Pen(mybrush, PenSizeX.Value), newx, prev)
                Case "Point to Point"
                    Try
                        If Not used Then
                            prev = New Point(Windows.Forms.Cursor.Position.X - Me.Left - (PenSizeX.Value / 2) - 10, Windows.Forms.Cursor.Position.Y - Me.Top - 30 - (PenSizeY.Value / 2))
                            used = True
                            Return
                        Else
                            newx = New Point(Windows.Forms.Cursor.Position.X - Me.Left - (PenSizeX.Value / 2) - 10, Windows.Forms.Cursor.Position.Y - Me.Top - 30 - (PenSizeY.Value / 2))
                        End If
                    Catch
                        prev = newx
                        newx = New Point(Windows.Forms.Cursor.Position.X - Me.Left - (PenSizeX.Value / 2) - 10, Windows.Forms.Cursor.Position.Y - Me.Top - 30 - (PenSizeY.Value / 2))
                    End Try
                    graphics.DrawLine(New Pen(mybrush, PenSizeX.Value), newx, prev)
                    Threading.Thread.Sleep(50)
                    prev = newx
            End Select

        End If
    End Sub



    Private Sub StoredColour1_Click(sender As Object, e As EventArgs) Handles StoredColour1.Click

        TBBlue.BackColor = StoredColour1.BackColor
        TBRed.BackColor = StoredColour1.BackColor
        TBGreen.BackColor = StoredColour1.BackColor
        Opacity.BackColor = StoredColour1.BackColor

        TBRed.Value = StoredColour1.BackColor.R
        TBGreen.Value = StoredColour1.BackColor.G
        TBBlue.Value = StoredColour1.BackColor.B


    End Sub

    Private Sub StoredColour2_Click(sender As Object, e As EventArgs) Handles StoredColour2.Click
        TBBlue.BackColor = StoredColour2.BackColor
        TBRed.BackColor = StoredColour2.BackColor
        TBGreen.BackColor = StoredColour2.BackColor
        Opacity.BackColor = StoredColour2.BackColor

        TBRed.Value = StoredColour2.BackColor.R
        TBGreen.Value = StoredColour2.BackColor.G
        TBBlue.Value = StoredColour2.BackColor.B


    End Sub

    Private Sub StoredColour3_Click(sender As Object, e As EventArgs) Handles StoredColour3.Click
        TBBlue.BackColor = StoredColour3.BackColor
        TBRed.BackColor = StoredColour3.BackColor
        TBGreen.BackColor = StoredColour3.BackColor
        Opacity.BackColor = StoredColour3.BackColor

        TBRed.Value = StoredColour3.BackColor.R
        TBGreen.Value = StoredColour3.BackColor.G
        TBBlue.Value = StoredColour3.BackColor.B

    End Sub

    Private Sub StoredColour4_Click(sender As Object, e As EventArgs) Handles StoredColour4.Click
        TBBlue.BackColor = StoredColour4.BackColor
        TBRed.BackColor = StoredColour4.BackColor
        TBGreen.BackColor = StoredColour4.BackColor
        Opacity.BackColor = StoredColour4.BackColor

        TBRed.Value = StoredColour4.BackColor.R
        TBGreen.Value = StoredColour4.BackColor.G
        TBBlue.Value = StoredColour4.BackColor.B
    End Sub

    Private Sub StoredColour5_Click(sender As Object, e As EventArgs) Handles StoredColour5.Click
        TBBlue.BackColor = StoredColour5.BackColor
        TBRed.BackColor = StoredColour5.BackColor
        TBGreen.BackColor = StoredColour5.BackColor
        Opacity.BackColor = StoredColour5.BackColor

        TBRed.Value = StoredColour5.BackColor.R
        TBGreen.Value = StoredColour5.BackColor.G
        TBBlue.Value = StoredColour5.BackColor.B
    End Sub

    Private Sub StoredColour6_Click(sender As Object, e As EventArgs) Handles StoredColour6.Click
        TBBlue.BackColor = StoredColour6.BackColor
        TBRed.BackColor = StoredColour6.BackColor
        TBGreen.BackColor = StoredColour6.BackColor
        Opacity.BackColor = StoredColour6.BackColor

        TBRed.Value = StoredColour6.BackColor.R
        TBGreen.Value = StoredColour6.BackColor.G
        TBBlue.Value = StoredColour6.BackColor.B
    End Sub

    Private Sub StoredColour7_Click(sender As Object, e As EventArgs) Handles StoredColour7.Click
        TBBlue.BackColor = StoredColour7.BackColor
        TBRed.BackColor = StoredColour7.BackColor
        TBGreen.BackColor = StoredColour7.BackColor
        Opacity.BackColor = StoredColour7.BackColor

        TBRed.Value = StoredColour7.BackColor.R
        TBGreen.Value = StoredColour7.BackColor.G
        TBBlue.Value = StoredColour7.BackColor.B
    End Sub

    Private Sub StoredColour8_Click(sender As Object, e As EventArgs) Handles StoredColour8.Click
        TBBlue.BackColor = StoredColour8.BackColor
        TBRed.BackColor = StoredColour8.BackColor
        TBGreen.BackColor = StoredColour8.BackColor
        Opacity.BackColor = StoredColour8.BackColor

        TBRed.Value = StoredColour4.BackColor.R
        TBGreen.Value = StoredColour4.BackColor.G
        TBBlue.Value = StoredColour4.BackColor.B
    End Sub
    Private Sub btnClearAll_Click(sender As Object, e As EventArgs) Handles btnClearAll.Click
        StoredColour1.BackColor = System.Drawing.Color.White
        StoredColour2.BackColor = System.Drawing.Color.White
        StoredColour3.BackColor = System.Drawing.Color.White
        StoredColour4.BackColor = System.Drawing.Color.White
        StoredColour5.BackColor = System.Drawing.Color.White
        StoredColour6.BackColor = System.Drawing.Color.White
        StoredColour7.BackColor = System.Drawing.Color.White
        StoredColour8.BackColor = System.Drawing.Color.White

        StoredColourNumber = 0
    End Sub

    Private Sub btnColourPicker_Click(sender As Object, e As EventArgs) Handles btnColourPicker.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            TBRed.Value = ColorDialog1.Color.R
            TBGreen.Value = ColorDialog1.Color.G
            TBBlue.Value = ColorDialog1.Color.B

            TBBlue.BackColor = Color.FromArgb(ColorDialog1.Color.R, ColorDialog1.Color.G, ColorDialog1.Color.B)
            TBRed.BackColor = Color.FromArgb(ColorDialog1.Color.R, ColorDialog1.Color.G, ColorDialog1.Color.B)
            TBGreen.BackColor = Color.FromArgb(ColorDialog1.Color.R, ColorDialog1.Color.G, ColorDialog1.Color.B)
            Opacity.BackColor = Color.FromArgb(ColorDialog1.Color.R, ColorDialog1.Color.G, ColorDialog1.Color.B)
        End If
    End Sub

    Private Sub ShowHideRGBValuesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowHideRGBValuesToolStripMenuItem.Click

        lblRedValue.Visible = Not lblRedValue.Visible
        lblGreenValue.Visible = Not lblGreenValue.Visible
        lblBlueValue.Visible = Not lblBlueValue.Visible
        lblOpacity.Visible = Not lblOpacity.Visible
    End Sub

    Private Sub btnClearOldest_Click(sender As Object, e As EventArgs) Handles btnClearOldest.Click

        If StoredColourNumber = 8 Then StoredColourNumber = 0

        If StoredColour1.BackColor <> System.Drawing.Color.White And StoredColour2.BackColor <> System.Drawing.Color.White And StoredColour3.BackColor <> System.Drawing.Color.White And StoredColour4.BackColor <> System.Drawing.Color.White And StoredColour5.BackColor <> System.Drawing.Color.White And StoredColour6.BackColor <> System.Drawing.Color.White And StoredColour7.BackColor <> System.Drawing.Color.White And StoredColour8.BackColor <> System.Drawing.Color.White And StoredColour8.BackColor <> TBBlue.BackColor Then
            StoredColourNumber = StoredColourNumber + 1
        End If

        Select Case StoredColourNumber

            Case 1
                StoredColour1.BackColor = System.Drawing.Color.White
            Case 2
                StoredColour2.BackColor = System.Drawing.Color.White
            Case 3
                StoredColour3.BackColor = System.Drawing.Color.White
            Case 4
                StoredColour4.BackColor = System.Drawing.Color.White
            Case 5
                StoredColour5.BackColor = System.Drawing.Color.White
            Case 6
                StoredColour6.BackColor = System.Drawing.Color.White
            Case 7
                StoredColour7.BackColor = System.Drawing.Color.White
            Case 8
                StoredColour8.BackColor = System.Drawing.Color.White
        End Select




    End Sub

    Private Sub ComboShape_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboShape.SelectedIndexChanged

        If ComboShape.SelectedItem = "Cluster" And lblClusterDesity.Visible = False Then
            lblCluster.Visible = Not lblCluster.Visible
            lblClusterDesity.Visible = Not lblClusterDesity.Visible
            TrackBarCluster.Visible = Not TrackBarCluster.Visible
            GroupBox1.Height = 240
            GroupBox2.Location = New Point(12, 300)

        End If

        If ComboShape.SelectedItem <> "Cluster" Then
            lblCluster.Visible = Not lblCluster.Visible
            lblClusterDesity.Visible = Not lblClusterDesity.Visible
            TrackBarCluster.Visible = Not TrackBarCluster.Visible
            GroupBox1.Height = 160
            GroupBox2.Location = New Point(12, 218)
        End If

    End Sub

    
    Private Sub TrackBarCluster_Scroll(sender As Object, e As EventArgs) Handles TrackBarCluster.Scroll
        lblClusterDesity.Text = TrackBarCluster.Value
    End Sub

    Private Sub SaveToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem1.Click

    End Sub
End Class


