This repository contains the code to create the 3D environments from a satellite image. Inside the scenes folder you will find 3 example scenes and the corresponding satellite image used to create this scene. Scene 2 demonstrates the ability to create landscapes by drawing your own. 

To see the scenes press the play button within Unity.

If you wish to change the mode of the buildings: 
1. Locate the Building Generator script on the 'World' asset within the scene
2. Change the type of the public enumerator variable. The 'BOUNDING' option creates the buildings using the geometry obtained from bounding boxes. The 'VERTEX' option creates the buildings using the geometry obtained from using the Corner Harris algorithm and the 'AREA' option creates buildings by instiating certain buildings depending on the area of the bounding boxes.


To create your own environment:

Within the Python Project:

1. Delete the contents of the 'Buildings' and 'Foliage' folder within the Python project. You will also need to delete the contents of the folders inside the 'Geometry' and 'Predictions' folders.
2. Swap out the image in the 'Image' folder with your own satellite image.
3. Run the 'ExtractFeatures.py' file, this will produce text files needed within Unity to create an environment.

Within the Unity Project:

1. Within Unity create a new scene and drag the 'World' prefab into the scene.
2. Within the Resources folder create a new folder and with the subdirectories 'Buildings' and 'Other'.
3. Drag the text files within the 'Foliage' folder from the python project into the 'Other' folder within Unity.
4. Drag the folders in the 'Geometry' folder from the python project into the 'Buildings' folder within Unity.
5. On the 'World' asset within the scene locate the 'Get Text Files' script and change the paths to correspond with folder you created within the 'Resources folder'.


Link to python project: https://github.com/b3-kennedy/CCTP-Python
