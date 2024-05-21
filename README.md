Purpose:
The purpose of this project is to develop a game simulation based on the principles of natural selection, where players can observe and interact with evolving populations of virtual organisms. 
The game will incorporate neural networks as an essential component to allow the organisms to adapt and learn from their environment. By providing players with a fun and engaging way to explore the concepts of
natural selection, we aim to increase their understanding of these topics and inspire interest in STEM fields. Through this project, we also hope to contribute to the research and development of more sophisticated neural network algorithms and their application to real-world problems.

Product Scope:
The natural selection game simulation will be a software application that allows players to interact with virtual organisms as they evolve over time through natural selection. The game will be designed to be fun,
engaging, and educational, providing players with a unique opportunity to learn about natural selection, and evolution overall.The game will feature a variety of settings and environments where the organisms will 
live and compete for survival, including different climate zones, food sources, and predators. The virtual organisms will be controlled by neural networks that will enable them to learn and adapt to their environment, 
allowing players to witness the effects of natural selection in action.The game will include several modes, including a single-player home environment building, where players can manipulate the environment, 
and a multiplayer mode, where players can compete against each other in online matches. The game will also include various customization options, such as the ability to modify the characteristics and behavior of the
environment in which they live.The natural selection game simulation will be compatible with desktop computers, and laptops and will be available for install from a standalone installer. The game will be designed to 
be intuitive and user-friendly, with clear instructions and tutorials to guide players through the gameplay.

Product Functions
1.	Create, customize and save virtual organisms with a variety of attributes such as physical traits, behavior, and neural network parameters.
2.	Simulate virtual environments with different climate zones, food sources, and predators.
3.	Allow players to interact with the virtual organisms by modifying the environment factors such as climate zones, food sources, and predators.
4.	Enable virtual organisms to learn and adapt to their environment using neural networks, allowing players to observe the effects of natural selection in action.
5.	Provide a single-player campaign mode, with progressively challenging levels where players can observe and interact with evolving populations of virtual organisms.
6.	Offer a multiplayer mode, where two to four players can compete against each other via LAN in an environment that the living conditions get worse with time.
7.	Provide a leaderboard system to track player performance and compare it against other players.
8.	Offer a variety of statistics and visualization tools to help players analyze the performance of their virtual organisms and understand the underlying mechanics of natural selection.
9.	Provide an intuitive and user-friendly interface, with clear instructions and tutorials to guide players through the gameplay.

Design and Implementation Constraints:
The natural selection game simulation has been designed with a number of constraints in mind to ensure that it is feasible to implement and meets the project's objectives. These constraints include:
1.	Top-Down 2D Design: The game will be implemented as a top-down 2D game, rather than a 3D game. This design choice was made to simplify the implementation process and reduce development time, while still providing an immersive and engaging experience for players.
2.	Neural Network Implementation: Neural networks are an essential component of the natural selection game simulation and will be implemented using a pre-existing neural network library. This will allow for efficient implementation and testing of neural network algorithms and reduce the amount of development time required to implement this functionality.
3.	Limited Multiplayer Support: The game will support multiplayer mode for a total of two to four players via LAN, rather than online multiplayer. This was done to simplify the implementation process and reduce the complexity of network programming, while still providing a fun and engaging multiplayer experience for players.
4.	Resource Management: The game will require the efficient management of computational resources, particularly with respect to the use of neural networks. Efforts will be made to optimize the code for efficient use of resources and to minimize memory usage, to ensure that the game can be run on a wide range of hardware configurations.
5.	Operating System Compatibility: The game will be developed and tested on Windows 10 and 11 operating systems. Efforts will be made to ensure that the game is compatible with other operating systems, but compatibility cannot be guaranteed.


Neural Network functionalities:

Initialization:
The learning process begins with the initialization of a diverse population of virtual creatures. Each creature is equipped with a neural network, represented by the NNet script. These neural networks are 
initialized with random weights and biases, forming the initial generation of creatures.

Evaluation and Fitness Calculation:
During each generation, the creatures navigate the simulated environment. The CreatureController script constantly evaluates their performance. The overall fitness of each creature is calculated, considering 
factors such as the distance traveled, health status, and responses to environmental stimuli like radioactive and food objects. This fitness score quantifies the creature's ability to navigate, survive, and adapt
to the environment.

Selection of Parents:
The genetic algorithm, managed by the GeneticManager script, selects parents for the next generation based on their fitness scores. Creatures with higher fitness are more likely to be chosen as parents.
To introduce diversity, the gene pool mechanism is utilized, allowing for a mix of high-fitness and lower-fitness creatures in the selection process.

Crossover (Recombination):
Selected parents undergo crossover operations, a process akin to biological recombination. During crossover, genetic material, represented by weights and biases in the neural networks, is exchanged between 
parent networks. This exchange of genetic information results in offspring that inherit a combination of successful traits from both parents.

Mutation:
To encourage exploration and prevent the premature convergence of the population, a mutation operation is introduced. Random changes are applied to the weights of the neural networks, introducing small and 
unpredictable adjustments. Mutation adds a level of variability, allowing the population to explore a broader solution space.

Population Update:
The new population, consisting of both parents and their offspring, replaces the previous generation. This process repeats for multiple generations, allowing the population to adapt and improve over time. 
The iterative nature of the genetic algorithm facilitates the continuous refinement of neural networks.

Emergent Behaviors:
Over successive generations, emergent behaviors manifest in the virtual creatures. These behaviors are a result of the adaptive learning process. Successful traits that contribute to better navigation and 
survival are passed on and refined. The neural networks learn to interpret environmental cues, responding with nuanced behaviors that enhance the creatures' overall fitness.

Performance Evaluation:
The success of the learning process is evaluated based on the performance of the evolved creatures. Metrics such as the efficiency of navigation, the ability to avoid hazards, and the overall adaptability of
the population are observed. The simulation provides insights into how the neural networks have learned to generate intelligent behaviors within the Unity environment.

Iterative Learning:
The learning process continues iteratively through multiple generations. As the population evolves, neural networks become increasingly sophisticated, leading to the emergence of more adaptive and intelligent
behaviors. This iterative learning cycle showcases the potential of evolutionary algorithms and neural networks in shaping the behavior of virtual creatures.


Adaptive Evolution:
The learning process demonstrates the adaptive evolution of the virtual creatures. Over time, the neural networks evolve to encode behaviors that optimize the creatures' chances of survival and efficient 
navigation. This adaptability is a key outcome of the evolutionary approach, allowing the population to respond dynamically to changes in the simulated environment.

Behavioral Complexity:
As the generations progress, the behavioral complexity of the virtual creatures increases. The neural networks learn intricate patterns and strategies, leading to creatures that can effectively interact with
and respond to various environmental stimuli. The emergent behaviors showcase the power of evolutionary algorithms in evolving complex and context-aware behaviors.

Fine-Tuning through Fitness:
The fitness-based selection ensures that beneficial traits are preferentially passed on to subsequent generations. Creatures with behaviors that contribute positively to their fitness, such as avoiding hazards
or efficiently reaching food sources, have a higher likelihood of becoming parents. This fine-tuning process through fitness maximizes the overall adaptability of the population.

Balancing Exploration and Exploitation:
The combination of crossover and mutation strikes a balance between exploration and exploitation of the solution space. Crossover leverages successful combinations of traits from parents, while mutation 
introduces random changes, preventing the population from converging too quickly to suboptimal solutions. This balance allows for the exploration of diverse solutions while exploiting successful adaptations.

Continuous Learning and Improvement:
The continuous nature of the learning process enables the virtual creatures to adapt not only to the initial environment but also to changes or challenges introduced over time. The iterative learning cycle 
ensures that the population is resilient and capable of responding to new and unforeseen circumstances.

Real-Time Simulation Insights:
The real-time nature of the Unity simulation provides valuable insights into the learning process. Observing the creatures' behaviors, tracking their performance metrics, and visualizing the evolutionary 
journey contribute to a better understanding of how neural networks can learn and adapt within dynamic virtual environments.

Potential Applications:
Beyond the simulated environment, the principles demonstrated in this neural network learning process have potential applications in real-world scenarios. Evolutionary algorithms and neural networks could
be employed in various fields, such as robotics, autonomous systems, and optimization problems, where adaptive behavior and learning are crucial.

Conclusion:
The described neural network learning process encapsulates a journey of evolution, adaptation, and emergent intelligence within a Unity simulation. It highlights the potential of combining evolutionary 
algorithms and neural networks to create virtual entities that exhibit complex and contextually aware behaviors. The insights gained from this process contribute to the broader understanding of artificial life and provide a foundation for future advancements in adaptive systems and artificial intelligence.





