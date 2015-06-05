<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSURF
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.gbChooseImageScene = New System.Windows.Forms.GroupBox()
        Me.rdoWebcam = New System.Windows.Forms.RadioButton()
        Me.rdoImageFile = New System.Windows.Forms.RadioButton()
        Me.lblImageScene = New System.Windows.Forms.Label()
        Me.lblImageToFind = New System.Windows.Forms.Label()
        Me.txtImageScene = New System.Windows.Forms.TextBox()
        Me.txtImageToFind = New System.Windows.Forms.TextBox()
        Me.btnImageScene = New System.Windows.Forms.Button()
        Me.btnImageToFind = New System.Windows.Forms.Button()
        Me.btnPerformSURFOrGetImageToTrack = New System.Windows.Forms.Button()
        Me.ckDrawKeyPoints = New System.Windows.Forms.CheckBox()
        Me.ckDrawMatchingLines = New System.Windows.Forms.CheckBox()
        Me.ibResult = New Emgu.CV.UI.ImageBox()
        Me.ofdImageScene = New System.Windows.Forms.OpenFileDialog()
        Me.ofdImageToFind = New System.Windows.Forms.OpenFileDialog()
        Me.gbChooseImageScene.SuspendLayout()
        CType(Me.ibResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbChooseImageScene
        '
        Me.gbChooseImageScene.Controls.Add(Me.rdoWebcam)
        Me.gbChooseImageScene.Controls.Add(Me.rdoImageFile)
        Me.gbChooseImageScene.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbChooseImageScene.Location = New System.Drawing.Point(27, 12)
        Me.gbChooseImageScene.Name = "gbChooseImageScene"
        Me.gbChooseImageScene.Size = New System.Drawing.Size(178, 111)
        Me.gbChooseImageScene.TabIndex = 0
        Me.gbChooseImageScene.TabStop = False
        Me.gbChooseImageScene.Text = "choose image source"
        '
        'rdoWebcam
        '
        Me.rdoWebcam.AutoSize = True
        Me.rdoWebcam.Location = New System.Drawing.Point(40, 86)
        Me.rdoWebcam.Name = "rdoWebcam"
        Me.rdoWebcam.Size = New System.Drawing.Size(77, 20)
        Me.rdoWebcam.TabIndex = 1
        Me.rdoWebcam.TabStop = True
        Me.rdoWebcam.Text = "webcam"
        Me.rdoWebcam.UseVisualStyleBackColor = True
        '
        'rdoImageFile
        '
        Me.rdoImageFile.AutoSize = True
        Me.rdoImageFile.Location = New System.Drawing.Point(33, 37)
        Me.rdoImageFile.Name = "rdoImageFile"
        Me.rdoImageFile.Size = New System.Drawing.Size(84, 20)
        Me.rdoImageFile.TabIndex = 0
        Me.rdoImageFile.TabStop = True
        Me.rdoImageFile.Text = "image file"
        Me.rdoImageFile.UseVisualStyleBackColor = True
        '
        'lblImageScene
        '
        Me.lblImageScene.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImageScene.Location = New System.Drawing.Point(228, 29)
        Me.lblImageScene.Name = "lblImageScene"
        Me.lblImageScene.Size = New System.Drawing.Size(164, 16)
        Me.lblImageScene.TabIndex = 1
        Me.lblImageScene.Text = "choose image scene:"
        Me.lblImageScene.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblImageToFind
        '
        Me.lblImageToFind.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImageToFind.Location = New System.Drawing.Point(211, 87)
        Me.lblImageToFind.Name = "lblImageToFind"
        Me.lblImageToFind.Size = New System.Drawing.Size(222, 16)
        Me.lblImageToFind.TabIndex = 2
        Me.lblImageToFind.Text = "choose sub-image to find in scene"
        Me.lblImageToFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtImageScene
        '
        Me.txtImageScene.BackColor = System.Drawing.SystemColors.Window
        Me.txtImageScene.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImageScene.Location = New System.Drawing.Point(437, 29)
        Me.txtImageScene.Name = "txtImageScene"
        Me.txtImageScene.ReadOnly = True
        Me.txtImageScene.Size = New System.Drawing.Size(147, 22)
        Me.txtImageScene.TabIndex = 3
        '
        'txtImageToFind
        '
        Me.txtImageToFind.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImageToFind.Location = New System.Drawing.Point(439, 87)
        Me.txtImageToFind.Name = "txtImageToFind"
        Me.txtImageToFind.Size = New System.Drawing.Size(145, 22)
        Me.txtImageToFind.TabIndex = 4
        '
        'btnImageScene
        '
        Me.btnImageScene.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImageScene.Location = New System.Drawing.Point(590, 26)
        Me.btnImageScene.Name = "btnImageScene"
        Me.btnImageScene.Size = New System.Drawing.Size(57, 23)
        Me.btnImageScene.TabIndex = 5
        Me.btnImageScene.Text = "......"
        Me.btnImageScene.UseVisualStyleBackColor = True
        '
        'btnImageToFind
        '
        Me.btnImageToFind.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImageToFind.Location = New System.Drawing.Point(590, 87)
        Me.btnImageToFind.Name = "btnImageToFind"
        Me.btnImageToFind.Size = New System.Drawing.Size(57, 23)
        Me.btnImageToFind.TabIndex = 6
        Me.btnImageToFind.Text = "....."
        Me.btnImageToFind.UseVisualStyleBackColor = True
        '
        'btnPerformSURFOrGetImageToTrack
        '
        Me.btnPerformSURFOrGetImageToTrack.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPerformSURFOrGetImageToTrack.Location = New System.Drawing.Point(667, 39)
        Me.btnPerformSURFOrGetImageToTrack.Name = "btnPerformSURFOrGetImageToTrack"
        Me.btnPerformSURFOrGetImageToTrack.Size = New System.Drawing.Size(117, 95)
        Me.btnPerformSURFOrGetImageToTrack.TabIndex = 7
        Me.btnPerformSURFOrGetImageToTrack.Text = "Perform Surf Detection"
        Me.btnPerformSURFOrGetImageToTrack.UseVisualStyleBackColor = True
        '
        'ckDrawKeyPoints
        '
        Me.ckDrawKeyPoints.AutoSize = True
        Me.ckDrawKeyPoints.Checked = True
        Me.ckDrawKeyPoints.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckDrawKeyPoints.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckDrawKeyPoints.Location = New System.Drawing.Point(231, 128)
        Me.ckDrawKeyPoints.Name = "ckDrawKeyPoints"
        Me.ckDrawKeyPoints.Size = New System.Drawing.Size(117, 20)
        Me.ckDrawKeyPoints.TabIndex = 8
        Me.ckDrawKeyPoints.Text = "draw keypoints"
        Me.ckDrawKeyPoints.UseVisualStyleBackColor = True
        '
        'ckDrawMatchingLines
        '
        Me.ckDrawMatchingLines.AutoSize = True
        Me.ckDrawMatchingLines.Checked = True
        Me.ckDrawMatchingLines.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckDrawMatchingLines.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckDrawMatchingLines.Location = New System.Drawing.Point(371, 128)
        Me.ckDrawMatchingLines.Name = "ckDrawMatchingLines"
        Me.ckDrawMatchingLines.Size = New System.Drawing.Size(144, 20)
        Me.ckDrawMatchingLines.TabIndex = 9
        Me.ckDrawMatchingLines.Text = "draw matching lines"
        Me.ckDrawMatchingLines.UseVisualStyleBackColor = True
        '
        'ibResult
        '
        Me.ibResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ibResult.Enabled = False
        Me.ibResult.Location = New System.Drawing.Point(12, 154)
        Me.ibResult.Name = "ibResult"
        Me.ibResult.Size = New System.Drawing.Size(772, 229)
        Me.ibResult.TabIndex = 2
        Me.ibResult.TabStop = False
        '
        'frmSURF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(813, 386)
        Me.Controls.Add(Me.ibResult)
        Me.Controls.Add(Me.ckDrawMatchingLines)
        Me.Controls.Add(Me.ckDrawKeyPoints)
        Me.Controls.Add(Me.btnPerformSURFOrGetImageToTrack)
        Me.Controls.Add(Me.btnImageToFind)
        Me.Controls.Add(Me.btnImageScene)
        Me.Controls.Add(Me.txtImageToFind)
        Me.Controls.Add(Me.txtImageScene)
        Me.Controls.Add(Me.lblImageToFind)
        Me.Controls.Add(Me.lblImageScene)
        Me.Controls.Add(Me.gbChooseImageScene)
        Me.Name = "frmSURF"
        Me.Text = "Instructions: use ""..."" buttons to choose both Image Files, thne press Perform SU" & _
    "RF Button"
        Me.gbChooseImageScene.ResumeLayout(False)
        Me.gbChooseImageScene.PerformLayout()
        CType(Me.ibResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gbChooseImageScene As System.Windows.Forms.GroupBox
    Friend WithEvents rdoWebcam As System.Windows.Forms.RadioButton
    Friend WithEvents rdoImageFile As System.Windows.Forms.RadioButton
    Friend WithEvents lblImageScene As System.Windows.Forms.Label
    Friend WithEvents lblImageToFind As System.Windows.Forms.Label
    Friend WithEvents txtImageScene As System.Windows.Forms.TextBox
    Friend WithEvents txtImageToFind As System.Windows.Forms.TextBox
    Friend WithEvents btnImageScene As System.Windows.Forms.Button
    Friend WithEvents btnImageToFind As System.Windows.Forms.Button
    Friend WithEvents btnPerformSURFOrGetImageToTrack As System.Windows.Forms.Button
    Friend WithEvents ckDrawKeyPoints As System.Windows.Forms.CheckBox
    Friend WithEvents ckDrawMatchingLines As System.Windows.Forms.CheckBox
    Friend WithEvents ibResult As Emgu.CV.UI.ImageBox
    Friend WithEvents ofdImageScene As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ofdImageToFind As System.Windows.Forms.OpenFileDialog

End Class
