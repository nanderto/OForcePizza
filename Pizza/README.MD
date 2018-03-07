# Pizza

Pizza is a command line application that interacts with the Pizza API. Download it and run it from any local file location, using the syntax outlined below

### Command Line
pizza --operation parameters

#### Operations
* getPizzas
* getpizza
* GetToppings
* AddTopping
* DeleteTopping
* DeletePizza
* addPizza
* addToppingToPizza
* getToppingsForPizza


#### Parameters
* n  name (of pizza or topping)
* t  topping (name of topping)
* d  description (of pizza)
* pi pizzaId
* ti toppingId
* h  help

#### Samples
pizza --getPizzas
pizza --getpizza -pi:3
pizza --getpizza 3
pizza --addtoppingtopizza -pi:3 -ti:12
pizza --addtoppingtopizza 3 12