Imports Microsoft.Win32

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
 
        OpenFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then

            System.Threading.Thread.Sleep(2000)
            Dim Pr As Process = Process.Start("CMD", "/c xcopy " & OpenFileDialog1.FileName & " /y")
            Pr.WaitForExit()
            Label11.Hide()
            TabControl1.SelectTab(1)
        Else

        End If


    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Application.Exit()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim mess = MsgBox("Changes made will not be saved!", 48 + 4, "Windows 11 Patcher")
        If mess = DialogResult.Yes Then
            Shell("dism /unmount-wim /mountdir:mount /discard", AppWinStyle.NormalFocus)
        Else

        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        System.Diagnostics.Process.Start("http://iddqd.c1.biz")
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Application.Exit()
    End Sub

    Private Sub LinkLabel3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        TabControl1.SelectTab(3)
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        TabControl1.SelectTab(1)
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim Info As New Process
        Info.StartInfo.FileName = "dism.exe"
        Info.StartInfo.Arguments = "/Get-WimInfo /WimFile:boot.wim"
        Info.StartInfo.StandardOutputEncoding = System.Text.Encoding.GetEncoding("cp866")
        Info.StartInfo.UseShellExecute = False
        Info.StartInfo.RedirectStandardOutput = True
        Info.StartInfo.RedirectStandardInput = True

        Info.StartInfo.CreateNoWindow = True
        Dim Out As String = ""
        Info.Start()
        Out = Info.StandardOutput.ReadToEnd()
        Info.Close()
        Info.Dispose()
        MsgBox(Out)
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
      

        Try
            Button8.Hide()
            Button5.Hide()
            Label4.Enabled = False
            Label5.Enabled = False
            Label3.Enabled = False
            LinkLabel3.Enabled = False
            Label11.Show()

            MsgBox("This may take some time, please wait for the program to complete!" & vbNewLine & "Press " & Chr(34) & "OK" & Chr(34) & " to continue...", 48 + 0, "Windows 11 Patcher")
            Me.Text = "Please wait..."

            IO.Directory.CreateDirectory("mount")

            Dim Mount As New Process
            Mount.StartInfo.UseShellExecute = False
            Mount.StartInfo.RedirectStandardOutput = True
            Mount.StartInfo.FileName = "dism.exe"
            Mount.StartInfo.Arguments = "/mount-wim /wimfile:boot.wim /index:" & NumericUpDown1.Value & " /mountdir:mount"
            Mount.StartInfo.CreateNoWindow = True
            Mount.StartInfo.StandardOutputEncoding = System.Text.Encoding.GetEncoding("cp866")
            Mount.Start()
            Dim MountOut As String = Mount.StandardOutput.ReadToEnd()
            'MsgBox(MountOut)
            Mount.WaitForExit()


            Dim Bypass As New Process
            Bypass.StartInfo.UseShellExecute = False
            Bypass.StartInfo.RedirectStandardOutput = True
            Bypass.StartInfo.FileName = "bypass.exe"
            Bypass.StartInfo.CreateNoWindow = True
            Bypass.Start()
            Bypass.WaitForExit()

            Dim UnReg As New Process
            UnReg.StartInfo.UseShellExecute = False
            UnReg.StartInfo.RedirectStandardOutput = True
            UnReg.StartInfo.FileName = "cmd"
            UnReg.StartInfo.Arguments = "/c reg unload HKLM\SysWinPE"
            UnReg.StartInfo.CreateNoWindow = True
            UnReg.StartInfo.StandardOutputEncoding = System.Text.Encoding.GetEncoding("cp866")
            UnReg.Start()
            Dim UnRegOut As String = UnReg.StandardOutput.ReadToEnd()
            'MsgBox(UnRegOut)
            UnReg.WaitForExit()


            Dim Commit As New Process
            Commit.StartInfo.UseShellExecute = False
            Commit.StartInfo.RedirectStandardOutput = True
            Commit.StartInfo.FileName = "cmd"
            Commit.StartInfo.Arguments = "/c dism /unmount-wim /mountdir:mount /commit"
            Commit.StartInfo.CreateNoWindow = True
            Commit.StartInfo.StandardOutputEncoding = System.Text.Encoding.GetEncoding("cp866")
            Commit.Start()
            Dim CommitOut As String = Commit.StandardOutput.ReadToEnd()
            'MsgBox(CommitOut)
            Commit.WaitForExit()
            Process.Start(Application.StartupPath)
            Label1.Text = "Work is done!"
            Me.Text = "Work is done!"
            Try
                My.Computer.Audio.Play(My.Resources.Music, AudioPlayMode.Background)
            Catch
            End Try
            Button5.Show()
            TabControl1.SelectTab(2)

        Catch
            Label11.Text = "Aborting..."
            MsgBox("Error! File " & Chr(34) & "bypass.exe" & Chr(34) & " not found! Aborting!" & vbNewLine & "Press " & Chr(34) & "OK" & Chr(34) & " to continue...", 48 + 0, "Windows 11 Patcher")
            Dim Discard As New Process
            Discard.StartInfo.UseShellExecute = False
            Discard.StartInfo.RedirectStandardOutput = True
            Discard.StartInfo.FileName = "cmd"
            Discard.StartInfo.Arguments = "/c dism /unmount-wim /mountdir:mount /discard"
            Discard.StartInfo.CreateNoWindow = True
            Discard.StartInfo.StandardOutputEncoding = System.Text.Encoding.GetEncoding("cp866")
            Discard.Start()
            Dim DiscardOut As String = Discard.StandardOutput.ReadToEnd()
            'MsgBox(DiscardOut)
            Discard.WaitForExit()
            Application.Exit()

        End Try

    End Sub
End Class
