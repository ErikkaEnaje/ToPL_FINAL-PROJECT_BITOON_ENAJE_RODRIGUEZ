Imports System.IO
Public Class FormOpenFile

    Dim writefile As StreamWriter
    Dim usersFile As String
    Dim readfile As String


    Public Sub CreateAndSaveCode_Button(sender As Object, e As EventArgs) Handles Button1.Click
        writefile = File.CreateText(TextBox1.Text)
        usersFile = TextBox2.Text
        writefile.WriteLine(usersFile)
        writefile.Close()
    End Sub

    Public Sub OpenCode_Button(sender As Object, e As EventArgs) Handles Button2.Click
        readfile = My.Computer.FileSystem.ReadAllText(TextBox1.Text)
        TextBox2.Text = readfile
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Label4.Width = 1000
        Label4.Text = "IMPORTANT: Before you click on Create And Save Source Code or Open Source Code from Files Button, make sure to type in the right file path and include your source code. This ensures everything works smoothly."

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        TextBox3.ReadOnly = True
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Label5.Text = "Example:C:\SourceCode\Filename.txt (Save as Text Files)"
    End Sub
End Class