﻿Imports Microsoft.VisualBasic.Windows.Forms.Controls.UI

Namespace Controls

    Public Class ImageButton : Implements UI.IImageButton(Of UI.ImageButton)

        Public Property UI As UI.ImageButton Implements IImageButton(Of UI.ImageButton).UI
            Get
                Return _uiRes
            End Get
            Set(value As UI.ImageButton)
                _uiRes = value
                Call Me.Invalidate()
            End Set
        End Property

        Dim _uiRes As UI.ImageButton
        Protected _status As ButtonState = ButtonState.Normal

        Public Event DoClick(sender As ImageButton)

        Protected Overridable Sub ImageButton_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
            _status = ButtonState.Flat
            Call Me.Invalidate()
        End Sub

        Protected Overridable Sub ImageButton_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
            _status = ButtonState.Normal
            Call Me.Invalidate()
        End Sub

        Protected Overridable Sub ImageButton_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            _status = ButtonState.Pushed
            Call Me.Invalidate()
        End Sub

        Protected Overridable Sub ImageButton_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
            _status = ButtonState.Normal
            Call Invalidate()
            Call __fireClick()
        End Sub

        Protected Sub __fireClick()
            RaiseEvent DoClick(Me)
        End Sub

        Protected Overridable Sub ImageButton_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
            Dim res As Image

            Select Case _status
                Case ButtonState.Normal : res = UI.Normal
                Case ButtonState.Pushed : res = UI.Press
                Case ButtonState.Flat : res = UI.Highlight
                Case Else
                    Throw New NotSupportedException(_status.ToString)
            End Select

            Call e.Graphics.DrawImage(res, New Rectangle(New Point, Size))
        End Sub

        Public Sub SetSize(size As Size) Implements IImageButton(Of UI.ImageButton).SetSize
            Me.Size = size
        End Sub
    End Class
End Namespace