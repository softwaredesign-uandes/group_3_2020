# Group 3 2020, PreSubmission 3
## To-Do List
### Due Date: 
- Wednesday 27th May, 23:59 (30%)
### Datasets Source:
- http://mansci-web.uai.cl/minelib/Datasets.xhtml
#### Instructions
Integrate with a block model visualization tool I created. To do this you will have to create a web API that exposes the block model information and allows the visualization tool to access the data via the API. For now you will do this locally in your computer, but we will later move to remote server both for your application and for the visualization tool.
You can download the code of the visualization tool here: https://github.com/softwaredesign-uandes/block_model_viewer
Just open the index.html file to start the tool (it wont do anything until you have your API running of course).
Here is video with a demo on how to use the tool: https://youtu.be/7TkJ3qqrbc8
More in detail, what you have to implement is:
The previous endpoints should work for all valid blockmodels in the dataset. For large block models (larger than a couple thousands of block) the tool will probably get very slow, so you have to use reblocked versions of the block models in those cases.
Try to implement the minimal change required to add these endpoints, it is not recommended nor required that you add a complete web framework for doing this. Depending on your language, try finding a library that allows this minimal web api possible.
	
#### Rubric
- (2 points): An endpoint that can be accesed via GET '/api/block_models/'  that allows to retrieve a list of block models loaded in your application. The json returned by this endpoint should have this format: an array of objects, with only one property, the name of the model
Example:
[{"name":"marvin"},{"name":"mclaughlin"},{"name":"sm2"}]
- (4 points):  An endpoint that can be accesed via GET '/api/block_models/<name>/blocks/  that returns the blocks of the block model with name <name> . The json returned by this endpoint should have this format: an array of objects, each object representing a block, containing all the properties of the block. The block must contain at least 3 properties called "x" "y" and "z" that represent the block indices. The names used for the rest of the properties are not relevant.
Example:
[{"au":0.0,"cu":0.0,"x":0,"y":0,"z":0},{"au":0.0,"cu":0.0,"x":0,"y":0,"z":1},{"au":0.0,"cu":0.0,"x":0,"y":0,"z":2}]