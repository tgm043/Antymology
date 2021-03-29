# Assignment 3: Antymology

## Evolutionary Algorithm

The evolutionary algorithm simulates 10 instances of ants behaving according to a neural network, then take the best 5 and produce offsprings for 10 instances for the next generation.

The ants see in an area around them (in terms of its score) and use this information to make decisions.

## Code

### Terrain
- Each block now has an innate score. AirBlock's score depends on the pheromones present inside.
- Pheromone diffuses over time.

### Ant Behaviour
- Ants must have some measure of health. When an ants health hits 0, it dies and needs to be removed from the simulation
- Every timestep, you must reduce each ants health by some fixed amount
- Ants can refill their health by consuming Mulch blocks. To consume a mulch block, and ant must be directly ontop of a mulch block. After consuming, the mulch block must be removed from the world.
- Ants cannot consume mulch if another ant is also on the same mulch block
- When moving from one black to another, ants are not allowed to move to a block that is greater than 2 units in height difference
- Ants are able to dig up parts of the world. To dig up some of the world, an ant must be directly ontop of the block. After digging, the block is removed from the map
- Ants cannot dig up a block of type ContainerBlock
- Ants standing on an AcidicBlock will have the rate at which their health decreases multiplied by 2.
- Ants may give some of their health to other ants occupying the same space (must be a zero-sum exchange)
- Among your ants must exists a singular queen ant who is responsible for producing nest blocks
- Producing a single nest block must cost the queen 1/3rd of her maximum health.
- No new ants can be created during each evaluation phase (you are allowed to create as many ants as you need for each new generation though).
- Ants can secrete pheromones, a 'positive' or a 'negative' pheromone into the air.
