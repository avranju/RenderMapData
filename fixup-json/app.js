"use strict";

var http = require("client-http");

function main() {
    http.get("http://picmeup.blob.core.windows.net/temp/india_state.json", function(json) {
        var data = JSON.parse(json);
        data.geometries.forEach(function(geom) {
            if(geom.type === "Polygon") {
                geom.coordinates = transformPolygons(geom.coordinates);
            }
            else if(geom.type === "MultiPolygon") {
                // flatten the polygons array to a single array
                var plist = [];
                geom.coordinates.forEach(function(p) {
                    var polygons = transformPolygons(p);
                    polygons.forEach(function(polygon) {
                        plist.push(polygon);
                    });
                });
                geom.coordinates = plist;
            }
        });

        //var polygons = data.geometries.filter(function(geom) {
        //    return geom.coordinates.length > 1 && geom.type === "Polygon";
        //});

        //var mpolygons = data.geometries.filter(function(geom) {
        //    return geom.coordinates.length > 1 && geom.type === "MultiPolygon";
        //});

        console.log(JSON.stringify(data, null, "  "));
    });
}

function transformPolygons(polygons) {
    return polygons.map(function(polygon) {
        return {
            points: polygon.map(function(c) {
                return {
                    x: c[0],
                    y: c[1]
                };
            })
        };
    });
}

main();
