Option Strict On
Imports System.Drawing
Imports System.Diagnostics
Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.Features2D
Imports Emgu.CV.Structure
Imports Emgu.CV.UI
Imports Emgu.CV.Util







Public Class frmSURF

    Dim blnFirstTimeInResizeEvent As Boolean = True
    Dim intOrigFormWidth As Integer
    Dim intOrigFormHeight As Integer
    Dim intOrigImageBoxWidth As Integer
    Dim intOrigImageBoxHeight As Integer

    Dim capWebcam As Capture
    Dim blnWebcamCapturingInProcess As Boolean = False

    Dim imgSceneColor As Image(Of Bgr, Byte) = Nothing
    Dim imgToFindColor As Image(Of Bgr, Byte) = Nothing
    Dim imgCopyOfImageToFindWithBorder As Image(Of Bgr, Byte) = Nothing

    Dim blnImageSceneLoaded As Boolean = False
    Dim blnImageToFindLoaded As Boolean = False

    Dim imgResult As Image(Of Bgr, Byte) = Nothing

    Dim bgrKeyPointsColor As Bgr = New Bgr(Color.Blue)
    Dim bgrMatchingLinesColor As Bgr = New Bgr(Color.Green)
    Dim bgrFoundImagecolor As Bgr = New Bgr(Color.Red)

    Dim stopwatch As Stopwatch = New Stopwatch()



    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        intOrigFormWidth = Me.Width
        intOrigFormHeight = Me.Height
        intOrigImageBoxWidth = ibResult.Width
        intOrigImageBoxHeight = ibResult.Height

        ' Add any initialization after the InitializeComponent() call.

    End Sub


  


    Private Sub frmSURF_Resize(sender As System.Object, e As System.EventArgs) Handles MyBase.Resize
        If (blnFirstTimeInResizeEvent = True) Then
            blnFirstTimeInResizeEvent = False
        Else
            ibResult.Width = Me.Width - (intOrigFormWidth - intOrigImageBoxWidth)
            ibResult.Height = Me.Height - (intOrigImageBoxHeight - intOrigImageBoxHeight)
        End If

    End Sub


    Private Sub rdoImageFile_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoImageFile.CheckedChanged
        If (rdoImageFile.Checked = True) Then
            If (blnWebcamCapturingInProcess = True) Then
                RemoveHandler Application.Idle, New EventHandler(AddressOf Me.PerformSURFDetectionAndUpdateGUI)
                blnWebcamCapturingInProcess = False
            End If

            imgSceneColor = Nothing
            imgToFindColor = Nothing
            imgCopyOfImageToFindWithBorder = Nothing
            imgResult = Nothing
            blnImageSceneLoaded = False
            blnImageToFindLoaded = False

            txtImageScene.Text = ""
            txtImageToFind.Text = ""
            ibResult.Image = Nothing

            Me.Text = "Instructions:use""....""buttons to choose both image files,then press PerformSURF button"
            btnPerformSURFOrGetImageToTrack.Text = "Perform SURF Detection"
            ibResult.Image = Nothing

            lblImageScene.Visible = True
            lblImageToFind.Visible = True
            txtImageScene.Visible = True
            txtImageToFind.Visible = True
            btnImageScene.Visible = True
            btnImageToFind.Visible = True


        End If
    End Sub


    Private Sub rdoWebcam_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoWebcam.CheckedChanged
        If (rdoWebcam.Checked = True) Then                                                       'if webcam was just chosen
            imgSceneColor = Nothing                                                     'reset class level variables
            imgToFindColor = Nothing                                                    '
            imgCopyOfImageToFindWithBorder = Nothing                    '
            imgResult = Nothing                                                             '
            blnImageSceneLoaded = False                                             '
            blnImageToFindLoaded = False                                            '

            txtImageScene.Text = ""                                             'reset form
            txtImageToFind.Text = ""                                            '
            ibResult.Image = Nothing                                            '

            Try                                                                                     'try
                capWebcam = New Capture()                                       'to instantiate Capture object for webcam
            Catch ex As Exception                                                   'if not successful
                Me.Text = ex.Message                                                'show error message on title bar
                Return                                                                          'and bail
            End Try

            Me.Text = "Instructions: hold image to track up to camera, then press ""get image to track"" "                  'update title bar text
            btnPerformSURFOrGetImageToTrack.Text = "get image to track"                                                                                         'update button text
            imgToFindColor = Nothing

            AddHandler Application.Idle, New EventHandler(AddressOf Me.PerformSURFDetectionAndUpdateGUI)            'add SURF function to application's list of tasks
            blnWebcamCapturingInProcess = True                                                                                                                              'update flag variable

            lblImageScene.Visible = False                                   'hide controls that are not used with webcam
            lblImageToFind.Visible = False                              '
            txtImageScene.Visible = False                                   '
            txtImageToFind.Visible = False                              '
            btnImageScene.Visible = False                                   '
            btnImageToFind.Visible = False                              '
        End If

    End Sub

    Private Sub btnImageScene_Click(sender As System.Object, e As System.EventArgs) Handles btnImageScene.Click
        Dim dialogResult As DialogResult = ofdImageScene.ShowDialog()                                       'bring up image scene file dialog box

        If (dialogResult = Windows.Forms.DialogResult.OK Or dialogResult = Windows.Forms.DialogResult.Yes) Then      'if OK or Yes was chosen
            txtImageScene.Text = ofdImageScene.FileName                                                                                                                         'write file name to text box
        Else                                                                                                                                                                                                            'if cancel was chosen
            Return                                                                                                                                                                                                  'bail
        End If

        Try
            imgSceneColor = New Image(Of Bgr, Byte)(txtImageScene.Text)                 'try to load scene image
        Catch ex As Exception                                                                                                   'if not successful
            Me.Text = ex.Message                                                                                                'show error message on title bar
            Return                                                                                                                          'and bail
        End Try

        blnImageSceneLoaded = True                                  'if we get here, scene image was loaded successfully, update member variable

        If (blnImageToFindLoaded = False) Then                                                                       'if to find image has not been loaded yet
            ibResult.Image = imgSceneColor                                                                              'show image scene we just loaded on image box
        Else                                                                                                                                        'if to find image has already been loaded
            ibResult.Image = imgSceneColor.ConcateHorizontal(imgCopyOfImageToFindWithBorder)        'concatenate image to find with border and scene image, show result on image box
        End If
    End Sub


    Private Sub btnImageToFind_Click(sender As System.Object, e As System.EventArgs) Handles btnImageToFind.Click
        Dim dialogResult As DialogResult = ofdImageToFind.ShowDialog()                          'bring up image to find dialog box

        If (dialogResult = Windows.Forms.DialogResult.OK Or dialogResult = Windows.Forms.DialogResult.Yes) Then          'if OK or Yes was chosen
            txtImageToFind.Text = ofdImageToFind.FileName                                                                                                                           'write file name to text box
        Else                                                                                                                                                                                                                'if cancel was chosen
            Return                                                                                                                                                                                                      'bail
        End If

        Try
            imgToFindColor = New Image(Of Bgr, Byte)(txtImageToFind.Text)                       'try to load to find image
        Catch ex As Exception                                                                                                           'if not successful
            Me.Text = ex.Message                                                                                                        'show error message on title bar
            Return                                                                                                                                  'and bail
        End Try

        blnImageToFindLoaded = True                             'if we get here, to find image loaded successfully, update member variable

        imgCopyOfImageToFindWithBorder = imgToFindColor.Copy()              'make a copy of the image to find, so we can draw on the copy, therefore not changing the original image to find
        'draw 2 pixel wide border around the copy of the image to find, use same color as box for found image
        imgCopyOfImageToFindWithBorder.Draw(New Rectangle(1, 1, imgCopyOfImageToFindWithBorder.Width - 3, imgCopyOfImageToFindWithBorder.Height - 3), bgrFoundImagecolor, 2)

        If (blnImageSceneLoaded = True) Then                                                                                                         'if scene image is already loaded
            ibResult.Image = imgSceneColor.ConcateHorizontal(imgCopyOfImageToFindWithBorder)            'concatenate to find image with border to scene image, show result on image box
        Else                                                                                                                                                                        'if scene image has not already been loaded
            ibResult.Image = imgCopyOfImageToFindWithBorder                                                                             'show to find image border we just loaded on image box
        End If
    End Sub



    Private Sub txtImageScene_TextChanged(sender As System.Object, e As System.EventArgs)
        txtImageScene.SelectionStart = txtImageScene.Text.Length
    End Sub


    Private Sub txtImageToFind_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtImageToFind.TextChanged
        txtImageToFind.SelectionStart = txtImageToFind.Text.Length
    End Sub

    Private Sub btnPerformSURFOrGetImageToTrack_Click(sender As System.Object, e As System.EventArgs) Handles btnPerformSURFOrGetImageToTrack.Click
        If (rdoImageFile.Checked = True) Then                                                                                                                'if using still images
            If (txtImageToFind.Text <> String.Empty And txtImageScene.Text <> String.Empty) Then             'check that something is entered in each image file text box
                PerformSURFDetectionAndUpdateGUI(New Object(), New EventArgs())                                                 'call Perform SURF function
            Else                                                                                                                                                                            'else
                Me.Text = "choose image files first, then choose Perform SURF Detection button"                 'remind user to choose both image files before choosing Perform SURF
            End If
        ElseIf (rdoWebcam.Checked = True) Then                                                                                                               'if using the webcam
            imgToFindColor = imgSceneColor.Resize(320, 240, INTER.CV_INTER_CUBIC, True)     'use most recent image from webcam, which will be in imgSceneColor, then shrink and save as new image to track
            Me.Text = "Instructions: to update image to track, hold image up to camera, then press ""update image to track"""           'update title bar text
            btnPerformSURFOrGetImageToTrack.Text = "update image to track"                                                      'update button text
        Else
            'should never get here
        End If
    End Sub

    Private Sub ckDrawKeyPoints_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ckDrawKeyPoints.CheckedChanged
        If (ckDrawKeyPoints.Checked = False) Then                                    'if draw key points was just unchecked
            ckDrawMatchingLines.Checked = False                                         'uncheck draw matching lines check box
            ckDrawMatchingLines.Enabled = False                                         'and disable draw matching lines check box
        ElseIf (ckDrawKeyPoints.Checked = True) Then                             'else if draw key points was just checked
            ckDrawMatchingLines.Enabled = True                                          're-enable draw matching lines check box
        End If

        If (rdoImageFile.Checked = True) Then                                                                                'if using image from file
            btnPerformSURFOrGetImageToTrack_Click(New Object(), New EventArgs())            'call SURF button click event to update image for draw key points check box change
        End If
    End Sub

    Private Sub ckDrawMatchingLines_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ckDrawMatchingLines.CheckedChanged
        If (rdoImageFile.Checked = True) Then                                                                                        'if using image from file
            btnPerformSURFOrGetImageToTrack_Click(New Object(), New EventArgs())                    'call SURF button click event to update image for draw matching lines check box change
        End If
    End Sub

    Sub PerformSURFDetectionAndUpdateGUI(sender As Object, arg As EventArgs)
        If (rdoImageFile.Checked = True) Then                            'if getting still image from file
            If (blnImageSceneLoaded = False Or blnImageToFindLoaded = False Or imgSceneColor Is Nothing Or imgToFindColor Is Nothing) Then       'if either flag variable indicates we do not have image or either is null
                Me.Text = "either or both images are not loaded or null, please choose both images before performing SURF"                                      'show error message on title bar
                Return                                                                                                                                                                                                                                              'and bail
            End If

            Me.Text = "processing, please wait . . ."                                   'show processing message on title bar
            Application.DoEvents()                                                                      'call DoEvents() to update form
            stopwatch.Restart()                                                                             'and start the stopwatch
        ElseIf (rdoWebcam.Checked = True) Then                           'else if getting image from webcam
            Try                                                                                                             'try
                imgSceneColor = capWebcam.QueryFrame()                                  'to get next frame
            Catch ex As Exception                                                                           'if not successful
                Me.Text = ex.Message                                                                        'show error message on title bar
                Return                                                                                                  'and bail
            End Try

            If (imgSceneColor Is Nothing) Then                                                   'if next frame was not read successfully from webcam into scene image variable
                Me.Text = "error, imgSceneColor Is Nothing"                         'show error message
                Return                                                                                                  'and bail
            End If

            If (imgToFindColor Is Nothing) Then                                              'if we do not have an image to track yet,
                ibResult.Image = imgSceneColor                                                  'show scene image on image box
                Return                                                                                                  'and bail, note that this is not necessarily an error , the user may have not chosen an image to track yet
            End If
        End If
        'if we get here, we know both color images are good, we can begin the SURF detection stuff . . .
        Dim surfDetector As SURFDetector = New SURFDetector(500, False)             'instantiate SURF object, params are threshold and extended flag

        Dim imgSceneGray As Image(Of Gray, Byte) = Nothing                                  'grayscale image scene
        Dim imgToFindGray As Image(Of Gray, Byte) = Nothing                                 'grayscale image to find

        Dim vkpSceneKeyPoints As VectorOfKeyPoint                                                       'vector of key points in scene image
        Dim vkpToFindKeyPoints As VectorOfKeyPoint                                                  'vector of key points in to find image

        Dim mtxSceneDescriptors As Matrix(Of Single)                                            'matrix of descriptors to be queried for nearest neighbors
        Dim mtxToFindDescriptors As Matrix(Of Single)                                           'matrix of descriptors for to find image

        Dim mtxMatchIndices As Matrix(Of Integer)                                                   'matrix of descriptor indices, will result from training descriptors (call to KnnMatch())
        Dim mtxDistance As Matrix(Of Single)                                                            'matrix of distance values, will result from training descriptors (call to KnnMatch())
        Dim mtxMask As Matrix(Of Byte)                                                                      'both input and output to function VoteForUniqueness(), indicates which row is valid for the matches

        Dim bruteForceMatcher As BruteForceMatcher(Of Single)               'for each descriptor in the first set, this matcher finds the closest descriptor in the second set by trying each one
        Dim homographyMatrix As HomographyMatrix = Nothing                  'used for ProjectPoints() function to set location of found image in scene image

        Dim intKNumNearestNeighbors As Integer = 2                                  'k, number of nearest neighbors to search for
        Dim dblUniquenessThreshold As Double = 0.8                                  'the distance difference ratio for a match to be considered unique

        Dim intNumNonZeroElements As Integer                        'used as return value for number of non-zero elements both in matrix mask, and also from call to GetHomographyMatrixFromMatchedFeatures()

        'params for use with call to VoteForSizeAndOrientation()
        Dim dblScaleIncrement As Double = 1.5                           'determines the difference in scale for neighboring bins
        Dim intRotationBins As Integer = 20                             'number of bins for rotation out of 360 deg, for example if set to 20, each bin covers 18 deg (20 * 18 = 360)

        Dim dblRansacReprojectionThreshold As Double = 2.0      'for use with GetHomographyMatrixFromMatchedFeatures(), the maximum allowed reprojection error to treat a point pair as an inlier

        Dim rectImageToFind As Rectangle                                'rectangle encompassing the entire image to find
        Dim ptfPointsF As PointF()                                          '4 points defining box around location of found image in scene, in float type (in VB, "Single" => float)
        Dim ptPoints As Point()                                                 '4 points defining box around location of found image in scene, in integer type

        imgSceneGray = imgSceneColor.Convert(Of Gray, Byte)()                       'convert scene color image to grayscale
        imgToFindGray = imgToFindColor.Convert(Of Gray, Byte)()                 'convert to find color image to grayscale

        vkpSceneKeyPoints = surfDetector.DetectKeyPointsRaw(imgSceneGray, Nothing)                                                      'detect the key points in the scene, 2nd param is a mask, use Nothing if not needed
        mtxSceneDescriptors = surfDetector.ComputeDescriptorsRaw(imgSceneGray, Nothing, vkpSceneKeyPoints)      'compute scene descriptor, params are scene image, mask, and image scene key points

        vkpToFindKeyPoints = surfDetector.DetectKeyPointsRaw(imgToFindGray, Nothing)                                                    'detect the key points in the image to find, 2nd param is a mask, use Nothing if not needed
        mtxToFindDescriptors = surfDetector.ComputeDescriptorsRaw(imgToFindGray, Nothing, vkpToFindKeyPoints)   'compute to find descriptor, params are to find image, mask, and image to find key points

        bruteForceMatcher = New BruteForceMatcher(Of Single)(DistanceType.L2)               'instantiate brute force matcher object with L2, squared Euclidean distance
        bruteForceMatcher.Add(mtxToFindDescriptors)                                                                 'add matrix for to find descriptors to brute force matcher

        mtxMatchIndices = New Matrix(Of Integer)(mtxSceneDescriptors.Rows, intKNumNearestNeighbors)     'instantiate the indices matrix, params are rows and columns
        mtxDistance = New Matrix(Of Single)(mtxSceneDescriptors.Rows, intKNumNearestNeighbors)              'instantiate the distance matrix, params are rows and columns

        bruteForceMatcher.KnnMatch(mtxSceneDescriptors, mtxMatchIndices, mtxDistance, intKNumNearestNeighbors, Nothing)     'find the k-nearest match, last param is a mask (use Nothing if not needed)

        mtxMask = New Matrix(Of Byte)(mtxDistance.Rows, 1)                          'instantiate matrix mask
        mtxMask.SetValue(255)                                                                                       'set value of all elements in mask matrix

        Features2DToolbox.VoteForUniqueness(mtxDistance, dblUniquenessThreshold, mtxMask)       'filter the matched features, such that if a match is not unique, it is rejected

        intNumNonZeroElements = CvInvoke.cvCountNonZero(mtxMask)                                        'get number of non-zero elements in mask matrix
        If (intNumNonZeroElements >= 4) Then                                                                                 'if at least 4,
            'eliminate the matched features whose scale and rotation do not agree with the majority's scale and rotation
            intNumNonZeroElements = Features2DToolbox.VoteForSizeAndOrientation(vkpToFindKeyPoints, vkpSceneKeyPoints, mtxMatchIndices, mtxMask, dblScaleIncrement, intRotationBins)

            If (intNumNonZeroElements >= 4) Then                 'if still at least 4 non-zero elements,
                'get the homography matrix using RANSAC (RANdom SAmple Consensus)
                homographyMatrix = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(vkpToFindKeyPoints, vkpSceneKeyPoints, mtxMatchIndices, mtxMask, dblRansacReprojectionThreshold)
            End If
        End If

        imgCopyOfImageToFindWithBorder = imgToFindColor.Copy()              'make a copy of the image to find, so we can draw on the copy, therefore no changing the original image to find
        'draw a 2 pixel wide border around the copy of the image to find, use same color as box for found image
        imgCopyOfImageToFindWithBorder.Draw(New Rectangle(1, 1, imgCopyOfImageToFindWithBorder.Width - 3, imgCopyOfImageToFindWithBorder.Height - 3), bgrFoundImagecolor, 2)

        'next we will draw the scene image and image to find together in the result image
        '3 conditionals are necessary, depenting on which check boxes are checked (draw key points and / or draw matching lines)
        If (ckDrawKeyPoints.Checked = True And ckDrawMatchingLines.Checked = True) Then                      'if drawing both key points & matching lines on result image,
            'use DrawMatches function, combines scene image and copy of image to find into result, then draws key points and matching lines all in one step
            imgResult = Features2DToolbox.DrawMatches(imgCopyOfImageToFindWithBorder, _
                                 vkpToFindKeyPoints, _
                                 imgSceneColor, _
                                 vkpSceneKeyPoints, _
                                 mtxMatchIndices, _
                                 bgrMatchingLinesColor, _
                                 bgrKeyPointsColor, _
                                 mtxMask, _
                                 Features2DToolbox.KeypointDrawType.DEFAULT)
        ElseIf (ckDrawKeyPoints.Checked = True And ckDrawMatchingLines.Checked = False) Then             'if drawing key points but not matching lines on result image,
            'draw scene with key points on result image,
            imgResult = Features2DToolbox.DrawKeypoints(imgSceneColor, vkpSceneKeyPoints, bgrKeyPointsColor, Features2DToolbox.KeypointDrawType.DEFAULT)
            'then draw key points on copy of image to find,
            imgCopyOfImageToFindWithBorder = Features2DToolbox.DrawKeypoints(imgCopyOfImageToFindWithBorder, vkpToFindKeyPoints, bgrKeyPointsColor, Features2DToolbox.KeypointDrawType.DEFAULT)
            imgResult = imgResult.ConcateHorizontal(imgCopyOfImageToFindWithBorder)                 'then concatenate copy of image to find onto result image
        ElseIf (ckDrawKeyPoints.Checked = False And ckDrawMatchingLines.Checked = False) Then            'if not drawing key points or matching lines,
            imgResult = imgSceneColor                                                                                                               'assign scene image to result image
            imgResult = imgResult.ConcateHorizontal(imgCopyOfImageToFindWithBorder)                 'then concatenate copy of image to find onto result image
        Else
            'should never get here
        End If

        If (homographyMatrix IsNot Nothing) Then     'check to make sure homographyMatrix is not null, this is necessary b/c in a moment we will call a homographyMatrix function
            'in this next portion we draw a border on the scene portion of the result image, around where the found object is located

            rectImageToFind.X = 0                                                                   'to start with, set the rectangle to be
            rectImageToFind.Y = 0                                                                   'the full size of the image to find
            rectImageToFind.Width = imgToFindGray.Width                     '
            rectImageToFind.Height = imgToFindGray.Height                   '
            'next, instantiate an array of PointFs corresponding to the rectangle
            ptfPointsF = New PointF() {New PointF(rectImageToFind.Left, rectImageToFind.Top), _
                          New PointF(rectImageToFind.Right, rectImageToFind.Top), _
                          New PointF(rectImageToFind.Right, rectImageToFind.Bottom), _
                          New PointF(rectImageToFind.Left, rectImageToFind.Bottom)}
            'ProjectPoints() will set ptfPointsF (pass by reference) to be the
            homographyMatrix.ProjectPoints(ptfPointsF)              'location of a box in the scene portion of the result image, where the box surrounds the image we are looking for

            'convert from type PointF() to type Point(), this is necessary at this step b/c ProjectPoints() takes type PointF() but DrawPolyline takes type Point()
            ptPoints = New Point() {Point.Round(ptfPointsF(0)), _
                         Point.Round(ptfPointsF(1)), _
                         Point.Round(ptfPointsF(2)), _
                         Point.Round(ptfPointsF(3))}

            imgResult.DrawPolyline(ptPoints, True, bgrFoundImagecolor, 2)               'draw border around found image in scene portion of result image
        End If
        'end of the SURF detection stuff, now we update the form . . .

        ibResult.Image = imgResult                                          'assign resulting image to image box on form

        If (rdoImageFile.Checked = True) Then                            'if using image from file,
            stopwatch.Stop()                                                                'stop the stopwatch and update the form title bar with the time
            Me.Text = "processing time = " + stopwatch.Elapsed.TotalSeconds.ToString() + " sec, done processing, choose another image if desired"
        End If
    End Sub

End Class
