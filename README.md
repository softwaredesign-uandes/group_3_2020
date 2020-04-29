# Group 3 2020, PreSubmission 2
## To-Do List
### Due Date: 
- Wednesday 29th April, 23:59 (30%)
### Datasets Source:
- http://mansci-web.uai.cl/minelib/Datasets.xhtml
#### Instructions
For the pre-subimission, you must implement a reblock function that takes a block model and creates a new block model that has been "reduced" by clumping together neigbour blocks. Some considerations
The reblock function will receive three integer parameters: rx, ry, rz which correspond to the reblocking factor for each dimension of the model. For example if rx=2, ry=2, rz=2  the algorithm will iterate over the block model starting from its 0,0,0 block and create a new block by clumping 2x2x2 neighbour blocks
Most block models do not have every block that can be iterated from 0,0,0 to max_x, max_y, max_z to allow the reblock function to work you will have to consider those missing block as "air" blocks, which means they will have a value of 0 in all of the block properties
When clumping together the blocks, if you reach the end of the block model, you will have to "extend" the model with air blocks and use those air blocks for the clumping
When a group of neighbour blocks is clumped to create a new block, the way the attributes of the new block are genereated depends on the type of the attribute. We will consider three types of attributes : continuous attributes , mass proportional attributes , and categorical attributes (you may need to ask the user to identify the type of some of the properties).
- To generate the new block value of a continuous attribute, you simply need to add the values of the clumped blocks. An example of a continuous attributeis mass: the mass of the new block will be the sum of the masses of the clumped blocks.
- To generate the new block value of a mass proportional attribute, you need to calculate the weighted average of the property, using the mass of the block as weight. An example of mass attribute is the grade when it is stored as %, ppm or other proportion. For example: if you have are clumping two blocks together, block 1 of mass 100 and grade 50%, block 2 of mass 20 and grade 80% the grade of the new block is (100 * 50 + 20 * 80) / (100 + 20) = 55%
- To generate the new block value of a categorical attribute you need to select the most repeated value in the clumped blocks (and if its a tie, choose one at random). An example of categorical attribute is destination which indicates where a block will be sent (0: x place, 1: y place, etc). In a case like this it doesnt make sense to add or average the property

	
#### Rubric
- (1 pt) Fix your program based on the feedback from Submission 1.
	
- (3 pt) Provide a command that allows reblocking an already loaded Block Model (and then allows querying the new reblocked model) 

- (2 pt) Write extensive tests for the reblock function that considers the most important cases