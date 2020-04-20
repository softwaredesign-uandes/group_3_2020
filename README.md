# Group 3 2020, Submission 1
## To-Do List
### Due Date: 
- Wednesday 22nd April, 23:59
### Datasets Source:
- http://mansci-web.uai.cl/minelib/Datasets.xhtml
#### Functionality (3 pts)
- (1 pt) Provide a Command Line Interface that allows loading all the block models in the Datasets (except sm2 which doesn’t have its column description). Define a way in which the column information is specified when loading a Block Model. Your solution should work for any Block Model that has all the minimum info, not only the ones in the Dataset. 

- (0.5 pt) Provide a Command Line Interface that allows retrieving the Number of Blocks of a stored block model with the following parameters:
  - <block_model_name> num_blocks

- (0.5 pt) Provide a Command Line Interface that allows retrieving the Mass in Kilograms of one block in a stored block model with the following parameters:
  - <block_model_name> <block_x> <block_y> <block_z> mass

- (0.5 pt) Provide a Command Line Interface that allows retrieving the Grade in Percentage for each Mineral of one block in a stored block model with the following parameters:
  - <block_model_name> <block_x> <block_y> <block_z> <mineral_name> grade

- (0.5 pt) Provide a Command Line Interface that allows retrieving all other attributes of one block in a stored block model with the following parameters:
  - <block_model_name> <block_x> <block_y> <block_z> <attribute_name>
	
#### Design (3 pts)
- (1.2 pt) Provide a clear layer separation between at least: Command Line Interface Layer, Domain Layer, Persistence Layer (Files and/or DB). 
	
- (0.6 pt) Represent as independent modules/classes the main Entities of the problem: Block Models, Blocks,  

- (1.2 pt) Write Unit Tests that follow the good principles of test design, and consider the feedback given for the group Pre-Submission