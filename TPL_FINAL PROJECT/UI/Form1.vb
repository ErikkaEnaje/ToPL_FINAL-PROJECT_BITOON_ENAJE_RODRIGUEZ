Imports FontAwesome.Sharp
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions

Public Class Form1

    Dim inputArray(5) As String
    Dim perSplit(5) As String
    Dim ctr As Integer
    Dim dataType As Integer
    Dim delimiter As Integer
    Dim assignment_Operator As Integer
    Dim identifier As Integer
    Dim value As Integer
    Dim lexical_Passed As Boolean
    Dim syntax_Passed As Boolean
    Dim semantic_Passed As Boolean
    Dim result As Integer
    Dim result1 As Double
    Dim regex_Letter As Regex = New Regex("^[a-zA-Z\s]+")
    Dim regex_Number As Regex = New Regex("[0-9]+")

    Private currentButton As IconButton
    Private leftBorderButton As Panel
    Public formOpenFile As New FormOpenFile

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        leftBorderButton = New Panel()
        leftBorderButton.Size = New Size(7, 60)
        PanelMenu.Controls.Add(leftBorderButton)

        Me.Text = String.Empty
        Me.ControlBox = False
        Me.DoubleBuffered = True
        Me.MaximizedBounds = Screen.PrimaryScreen.WorkingArea
    End Sub

    Private Sub ActivateButton(senderBtn As Object, customColor As Color)
        If senderBtn IsNot Nothing Then
            DisableButton()

            currentButton = CType(senderBtn, IconButton)
            currentButton.BackColor = Color.FromArgb(37, 36, 81)
            currentButton.ForeColor = customColor
            currentButton.TextAlign = ContentAlignment.MiddleCenter

            leftBorderButton.BackColor = customColor
            leftBorderButton.Location = New Point(0, currentButton.Location.Y)
            leftBorderButton.Visible = True
            leftBorderButton.BringToFront()
        End If
    End Sub

    Private Sub DisableButton()
        If currentButton IsNot Nothing Then
            currentButton.BackColor = Color.FromArgb(31, 30, 68)
            currentButton.ForeColor = Color.Gainsboro
            currentButton.TextAlign = ContentAlignment.MiddleLeft
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If formOpenFile IsNot Nothing Then
            formOpenFile.Close()
        End If
        Reset()
    End Sub

    Private Sub Reset()
        DisableButton()
        leftBorderButton.Visible = False
    End Sub

    Private Sub OpenChildForm(childForm As Form)
        If formOpenFile IsNot Nothing Then
            formOpenFile.Close()
        End If

        formOpenFile = childForm
        childForm.TopLevel = False
        childForm.FormBorderStyle = FormBorderStyle.None
        childForm.Dock = DockStyle.Fill
        PanelDesktop.Controls.Add(childForm)
        PanelDesktop.Tag = childForm
        childForm.BringToFront()
        childForm.Show()
    End Sub

    Private Sub OpenFile_Button(sender As Object, e As EventArgs) Handles IconButton1.Click
        ActivateButton(sender, RCGColors.Color1)
        OpenChildForm(New FormOpenFile)

    End Sub

    Public Sub Lexical_Button(sender As Object, e As EventArgs) Handles IconButton2.Click
        ActivateButton(sender, RCGColors.Color2)

        inputArray = formOpenFile.TextBox2.Text.Split()

        For ctr = 0 To inputArray.Length - 1

            If inputArray(ctr) = "String" Or inputArray(ctr) = "int" Or inputArray(ctr) = "double" Or inputArray(ctr) = "char" Or inputArray(ctr) = "Boolean" Then
                perSplit(ctr) = "Data_Type"
                dataType += 1

            ElseIf inputArray(ctr) = ";" Then
                perSplit(ctr) = "Delimeter"
                delimiter += 1

            ElseIf inputArray(ctr) = "=" Then
                perSplit(ctr) = "Assignment_Operator"
                assignment_Operator += 1

            ElseIf regex_Letter.IsMatch(inputArray(ctr)) Then
                perSplit(ctr) = "Identifier"
                identifier += 1

            ElseIf inputArray(ctr).StartsWith("""") And inputArray(ctr).EndsWith("""") Or inputArray(ctr).StartsWith("'") And inputArray(ctr).EndsWith("'") Or inputArray(ctr).Contains(".") Or regex_Number.IsMatch(inputArray(ctr)) Then
                perSplit(ctr) = "Value"
                value += 1
            End If

        Next

        If dataType = 1 And delimiter = 1 And assignment_Operator = 1 And identifier = 1 And value = 1 Then
            lexical_Passed = True
            formOpenFile.TextBox3.Text = "LEXICAL ANALYSIS PASSED: Your code has been successfully analyzed for lexical correctness. No issues were found. You can proceed confidently with further development."
        Else
            lexical_Passed = False
            formOpenFile.TextBox3.Text = "LEXICAL ANALYSIS FAILED: Unable to process the lexical elements of the provided input. Please review your code for any lexical errors and make the necessary corrections before reattempting."
            formOpenFile.TextBox3.ForeColor = Color.Red
            IconButton3.Enabled = False
            IconButton4.Enabled = False
        End If
    End Sub

    Private Sub Syntax_Button(sender As Object, e As EventArgs) Handles IconButton3.Click
        ActivateButton(sender, RCGColors.Color3)

        If perSplit(0) = "Data_Type" And perSplit(1) = "Identifier" And perSplit(2) = "Assignment_Operator" And perSplit(3) = "Value" And perSplit(4) = "Delimeter" Then
            syntax_Passed = True
            formOpenFile.TextBox3.Text = "SYNTAX ANALYSIS PASSED: Your code has been successfully analyzed and meets the required syntax standards. No errors were detected. You may proceed with confidence."
        Else
            syntax_Passed = False
            IconButton4.Enabled = False
            formOpenFile.TextBox3.Text = "SYNTAX ANALYSIS FAILED: Unable to interpret the structure of the provided code. Please review your syntax for errors and try again."
            formOpenFile.TextBox3.ForeColor = Color.Red
        End If
    End Sub

    Private Sub Semantic_Button(sender As Object, e As EventArgs) Handles IconButton4.Click
        ActivateButton(sender, RCGColors.Color4)

        inputArray = formOpenFile.TextBox2.Text.Split()

        If inputArray(0) = "double" And inputArray(3).Contains(".") And regex_Number.IsMatch(inputArray(3)) Then
            semantic_Passed = True
            formOpenFile.TextBox3.Text = "SEMANTIC ANALYSIS PASSED: Your code has been successfully analyzed for meaning and logic. No semantic errors were detected. Your program is ready for execution without any issues."

        ElseIf inputArray(0) = "int" And regex_Number.IsMatch(inputArray(3)) Then
            semantic_Passed = True
            formOpenFile.TextBox3.Text = "SEMANTIC ANALYSIS PASSED: Your code has been successfully analyzed for meaning and logic. No semantic errors were detected. Your program is ready for execution without any issues."

        ElseIf inputArray(0) = "String" And inputArray(3).StartsWith("""") And inputArray(3).EndsWith("""") Then
            semantic_Passed = True
            formOpenFile.TextBox3.Text = "SEMANTIC ANALYSIS PASSED: Your code has been successfully analyzed for meaning and logic. No semantic errors were detected. Your program is ready for execution without any issues."

        ElseIf inputArray(0) = "boolean" And inputArray(3) = "True" Or inputArray(3) = "False" Then
            semantic_Passed = True
            formOpenFile.TextBox3.Text = "SEMANTIC ANALYSIS PASSED: Your code has been successfully analyzed for meaning and logic. No semantic errors were detected. Your program is ready for execution without any issues."

        ElseIf inputArray(0) = "char" And inputArray(3).StartsWith("'") And inputArray(3).EndsWith("'") Then
            semantic_Passed = True
            formOpenFile.TextBox3.Text = "SEMANTIC ANALYSIS PASSED: Your code has been successfully analyzed for meaning and logic. No semantic errors were detected. Your program is ready for execution without any issues."

        Else
            semantic_Passed = False
            formOpenFile.TextBox3.Text = "SEMANTIC ANALYSIS FAILED: The system faced challenges interpreting the meaning or logic within your code during semantic analysis. Kindly review and address any semantic errors for proper functionality."
            formOpenFile.TextBox3.ForeColor = Color.Red
        End If
    End Sub

    Private Sub Clear_Button(sender As Object, e As EventArgs) Handles IconButton5.Click
        ActivateButton(sender, RCGColors.Color5)

        Application.Exit()
        Dim startInfo As New ProcessStartInfo(Application.ExecutablePath)
        Process.Start(startInfo)
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If WindowState = FormWindowState.Maximized Then
            FormBorderStyle = FormBorderStyle.None
        Else
            FormBorderStyle = FormBorderStyle.Sizable
        End If
    End Sub


    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture()
    End Sub
    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(ByVal hWnd As System.IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer)
    End Sub
    Private Sub PanelTitleBar_MouseDown(sender As Object, e As MouseEventArgs) Handles PanelTitleBar.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub

    Private Sub Minimized_Button(sender As Object, e As EventArgs) Handles IconButton6.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Maximized_Button(sender As Object, e As EventArgs) Handles IconButton7.Click
        If WindowState = FormWindowState.Normal Then
            WindowState = FormWindowState.Maximized
        Else
            WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub Exit_Button(sender As Object, e As EventArgs) Handles IconButton8.Click
        Application.Exit()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
    End Sub


End Class
