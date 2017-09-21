//------------------------------------------------------------------
// (c) Copywrite Jianzhong Zhang
// This code is under The Code Project Open License
// Please read the attached license document before using this class
//------------------------------------------------------------------

// window class for testing 3d chart using WPF
// version 0.1

using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System;
using System.Collections;

using System.Data;

namespace WPFChart3D
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        // transform class object for rotate the 3d model
        public WPFChart3D.TransformMatrix m_transformMatrix = new WPFChart3D.TransformMatrix();

        // ***************************** 3d chart ***************************
        private WPFChart3D.Chart3D m_3dChart;       // data for 3d chart
        public int m_nChartModelIndex = -1;         // model index in the Viewport3d
        public int m_nSurfaceChartGridNo = 100;     // surface chart grid no. in each axis
        public int m_nScatterPlotDataNo = 5000;     // total data number of the scatter plot

        // ***************************** selection rect ***************************
        ViewportRect m_selectRect = new ViewportRect();
        public int m_nRectModelIndex = -1;
        //
        DataTable lScbuData;

        public Window1()
        {
            InitializeComponent();

            // selection rect
            m_selectRect.SetRect(new Point(-0.5, -0.5), new Point(-0.5, -0.5));
            WPFChart3D.Model3D model3d = new WPFChart3D.Model3D();
            ArrayList meshs = m_selectRect.GetMeshes();
            m_nRectModelIndex = model3d.UpdateModel(meshs, null, m_nRectModelIndex, this.mainViewport);

            // display surface chart
            TestSimpleScatterPlot(1000);
            TransformChart();
        }

        public Window1(DataTable SbcuData)
        {
            InitializeComponent();
            //
            lScbuData = SbcuData;
            // selection rect
            m_selectRect.SetRect(new Point(-0.5, -0.5), new Point(-0.5, -0.5));
            WPFChart3D.Model3D model3d = new WPFChart3D.Model3D();
            ArrayList meshs = m_selectRect.GetMeshes();
            m_nRectModelIndex = model3d.UpdateModel(meshs, null, m_nRectModelIndex, this.mainViewport);

            // display surface chart
            DispalaySBCUData(lScbuData);
            TransformChart();
        }

        private float doubleTofloat(double input)
        {
            float result = (float)input;
            if (float.IsPositiveInfinity(result))
            {
                result = float.MaxValue;
            }
            else if (float.IsNegativeInfinity(result))
            {
                result = float.MinValue;
            }
            return result;
        }

        public void DispalaySBCUData(DataTable dt)
        {
            // 1. set the scatter plot size
            m_3dChart = new ScatterChart3D();
            m_3dChart.SetDataNo(dt.Rows.Count); //-2 foefel met de lege velden 

            // 2. set the properties of each dot
            Random randomObject = new Random();

            foreach (DataRow row in dt.Rows)            
            {

                    ScatterPlotItem plotItem = new ScatterPlotItem();
                    plotItem.w = 10;
                    plotItem.h = 10;

                    float correctionFactor = 25;

                    plotItem.x = doubleTofloat((double)row["ToolX"]) * correctionFactor;
                    plotItem.y = doubleTofloat((double)row["ToolY"]) * correctionFactor;
                    plotItem.z = doubleTofloat((double)row["ToolZ"]) * correctionFactor;
             
                    plotItem.shape = (int)Chart3D.SHAPE.CONE;

                    if (cb_colormode.IsChecked == false)  //color mode
                    {
                        try
                        {
                            if ((bool)row["Longcheck"] == true)//shortcheck blue longcheck red.
                            {
                                plotItem.color = Color.FromRgb(255, 0, 0);
                            }
                            else
                            {
                                plotItem.color = Color.FromRgb(0, 0, 255);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            plotItem.color = Color.FromRgb(255, 0, 0);
                        }
                        
                    }
                    else //random color mode
                    {
                        Byte nR = (Byte)randomObject.Next(256);
                        Byte nG = (Byte)randomObject.Next(256);
                        Byte nB = (Byte)randomObject.Next(256);
                        plotItem.color = Color.FromRgb(nR, nG, nB);
                    }

                    ((ScatterChart3D)m_3dChart).SetVertex(dt.Rows.IndexOf(row), plotItem);
            }
            // 3. set axes
            m_3dChart.GetDataRange();
            m_3dChart.SetAxes(1);

            // 4. Get Mesh3D array from scatter plot
            ArrayList meshs = ((ScatterChart3D)m_3dChart).GetMeshes();

            // 6. show 3D scatter plot in Viewport3d
            WPFChart3D.Model3D model3d = new WPFChart3D.Model3D();
            m_nChartModelIndex = model3d.UpdateModel(meshs, null, m_nChartModelIndex, this.mainViewport);

            // 7. set projection matrix
            //from flange
            if (cb_FromFlange.IsChecked == true)
            {
                m_transformMatrix.CalculateProjectionMatrix(
                    0
                  , m_3dChart.XMax()
                  , 0
                  , m_3dChart.YMax()
                  , 0
                  , m_3dChart.ZMax()
                  , 0.09);
            }
            else
            {
                //zoom to range 
                m_transformMatrix.CalculateProjectionMatrix(
                    m_3dChart.XMin()
                  , m_3dChart.XMax()
                  , m_3dChart.YMin()
                  , m_3dChart.YMax()
                  , m_3dChart.ZMin()
                  , m_3dChart.ZMax()
                  , 0.09);
            }
            TransformChart();
        }

        // function for set a scatter plot, every dot is just a simple pyramid.
        public void TestSimpleScatterPlot(int nDotNo)
        {
            // 1. set the scatter plot size
            m_3dChart = new ScatterChart3D();
            m_3dChart.SetDataNo(nDotNo);

            // 2. set the properties of each dot
            Random randomObject = new Random();
            int nDataRange = 5000;
            for (int i = 0; i < nDotNo; i++)
            {
                ScatterPlotItem plotItem = new ScatterPlotItem();

                plotItem.w = 50;
                plotItem.h = 50;

                plotItem.x = (float)randomObject.Next(nDataRange);
                plotItem.y = (float)randomObject.Next(nDataRange);
                plotItem.z = (float)randomObject.Next(nDataRange);

                plotItem.shape = (int)Chart3D.SHAPE.ELLIPSE;

                Byte nR = (Byte)randomObject.Next(256);
                Byte nG = (Byte)randomObject.Next(256);
                Byte nB = (Byte)randomObject.Next(256);

                plotItem.color = Color.FromRgb(nR, nG, nB);
                ((ScatterChart3D)m_3dChart).SetVertex(i, plotItem);
            }
            // 3. set axes
            m_3dChart.GetDataRange();
            m_3dChart.SetAxes();

            // 4. Get Mesh3D array from scatter plot
            ArrayList meshs = ((ScatterChart3D)m_3dChart).GetMeshes();


            // 6. show 3D scatter plot in Viewport3d
            WPFChart3D.Model3D model3d = new WPFChart3D.Model3D();
            m_nChartModelIndex = model3d.UpdateModel(meshs, null, m_nChartModelIndex, this.mainViewport);

            // 7. set projection matrix
            float viewRange = (float)nDataRange;
            m_transformMatrix.CalculateProjectionMatrix(0, viewRange, 0, viewRange, 0, viewRange, 0.5);
            TransformChart();
        }


        public void OnViewportMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs args)
        {
            Point pt = args.GetPosition(mainViewport);
            if (args.ChangedButton == MouseButton.Left)         // rotate or drag 3d model
            {
                m_transformMatrix.OnLBtnDown(pt);
            }
            else if (args.ChangedButton == MouseButton.Right)   // select rect
            {
                m_selectRect.OnMouseDown(pt, mainViewport, m_nRectModelIndex);
            }
        }

        public void OnViewportMouseMove(object sender, System.Windows.Input.MouseEventArgs args)
        {
            Point pt = args.GetPosition(mainViewport);

            if (args.LeftButton == MouseButtonState.Pressed)                // rotate or drag 3d model
            {
                m_transformMatrix.OnMouseMove(pt, mainViewport);

                TransformChart();
            }
            else if (args.RightButton == MouseButtonState.Pressed)          // select rect
            {
                m_selectRect.OnMouseMove(pt, mainViewport, m_nRectModelIndex);
            }
            else
            {
                /*
                String s1;
                Point pt2 = m_transformMatrix.VertexToScreenPt(new Point3D(0.5, 0.5, 0.3), mainViewport);
                s1 = string.Format("Screen:({0:d},{1:d}), Predicated: ({2:d}, H:{3:d})", 
                    (int)pt.X, (int)pt.Y, (int)pt2.X, (int)pt2.Y);
                this.statusPane.Text = s1;
                */
            }
        }

        public void OnViewportMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs args)
        {
            Point pt = args.GetPosition(mainViewport);
            if (args.ChangedButton == MouseButton.Left)
            {
                m_transformMatrix.OnLBtnUp();
            }
            else if (args.ChangedButton == MouseButton.Right)
            {
                if (m_nChartModelIndex == -1) return;
                // 1. get the mesh structure related to the selection rect
                MeshGeometry3D meshGeometry = Model3D.GetGeometry(mainViewport, m_nChartModelIndex);
                if (meshGeometry == null) return;
              
                // 2. set selection in 3d chart
                m_3dChart.Select(m_selectRect, m_transformMatrix, mainViewport);

                // 3. update selection display
                m_3dChart.HighlightSelection(meshGeometry, Color.FromRgb(200, 200, 200));
            }
        }

        // zoom in 3d display
        public void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs args)
        {
            m_transformMatrix.OnKeyDown(args);
            TransformChart();
        }

        // this function is used to rotate, drag and zoom the 3d chart
        private void TransformChart()
        {
            if (m_nChartModelIndex == -1) return;
            ModelVisual3D visual3d = (ModelVisual3D)(this.mainViewport.Children[m_nChartModelIndex]);
            if (visual3d.Content == null) return;
            Transform3DGroup group1 = visual3d.Content.Transform as Transform3DGroup;
            group1.Children.Clear();
            group1.Children.Add(new MatrixTransform3D(m_transformMatrix.m_totalMatrix));
        }

        private void cb_FromFlange_Click(object sender, RoutedEventArgs e)
        {
            DispalaySBCUData(lScbuData);
            TransformChart();
        }

        private void cb_colormode_Click(object sender, RoutedEventArgs e)
        {
            DispalaySBCUData(lScbuData);
            TransformChart();
        }

    }
}
