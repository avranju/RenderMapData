using Newtonsoft.Json;
using RenderMapData.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RenderMapData
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Init();
        }

        async void Init()
        {
            // load the shape data and render polylines
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/states.json"));
            using (var stream = (await file.OpenSequentialReadAsync()).AsStreamForRead())
            using (var reader = new StreamReader(stream))
            {
                var json = await reader.ReadToEndAsync();
                var shapeData = JsonConvert.DeserializeObject<ShapeData>(json);

                foreach (var geometry in shapeData.Geometries)
                {
                    var figure = new PathFigure();
                    figure.StartPoint = new Windows.Foundation.Point(
                        geometry.Polygons[0].Points[0].X,
                        geometry.Polygons[0].Points[0].Y);

                    foreach (var polygon in geometry.Polygons)
                    {
                        var plSegment = new PolyLineSegment();
                        for (int i = 1; i < polygon.Points.Length; ++i)
                        {
                            plSegment.Points.Add(new Windows.Foundation.Point(polygon.Points[i].X, polygon.Points[i].Y));
                        }
                        figure.Segments.Add(plSegment);
                    }

                    mapPathGeometry.Figures.Add(figure);
                }
            }
        }
    }
}
