Adding vector asset https://developer.android.com/studio/write/vector-asset-studio.html
Add background color to vector image: https://stackoverflow.com/questions/48104161/how-to-add-a-background-to-a-vector-asset-that-i-want-to-use-as-my-apps-icon
Building adaptive icons: https://medium.com/androiddevelopers/vectordrawable-adaptive-icons-3fed3d3205b5

1) Use inkscape to save a square plain .svg file.  Play with sizing in document properties 
   to get it to center when imported into Android Asset Studio.  90 x 90 with top and left 
   margins = 0 and bottom and right margins = 10 worked once...
2) Open Android Asset Studio
	* Open Android Studio
	* view > tool windows > project
	* right-click on res folder
	* new > Vector Asset
3) Import .svg created in step 1)
	* Select Local path radio button
	* Select file with Path: file picker
	* Next.. Finish
	* .xml file is created in res/drawable folder
4) Add file to project
	* In \CycleTrip\CycleTrip.Android\Resources\drawable folder, right-click New.. Existing item
	* For menu item, edit \CycleTrip\CycleTrip.Android\Views\MainView.cs
	* In MenuItem class, add IconId = Resource.Drawable.ic_location_black
