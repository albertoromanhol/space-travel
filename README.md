# SPACE TRAVEL APPLICATION


## Overview

A .Net desktop app, using WPF to allow someone to plan and visualize a space travel from earth, with a spacecraft.

When you got in the application, you need to select a spacecraft, in the dropdown, and the slider will update the max allow value to the capacity of the spacecraft. So, you can select a spacecraft and define the number of passengers that will travel. Note that the default value is one, because we don't want to the spacecraft travel alone 🙂.

## The code

In this section we are going through the code itself.

### How to run the code

I make all the code using .NET 6, so you will need to have it install to run.

Just download or clone the repository folder, then you should open in a C# compiler that you like and run the WPF project. In my case, I develop and run the component in the VisualStudio.

#### The Logic Project

It's just a project that will handle the logic of the space travel. It has an Interface folder for the `SpaceTravelApplication`, a Model folder that contains the models that are used in all calculation itself, you see there the Spacecraft, Planets and a Result model, to give us the result message with some other attributes if we need to use it later.

In the `SpaceTravelApplication` it contains the logic itself. First, the program calculate the real travel distance, discounting the difference because of the number of passengers, until it has the maximum capacity and lose 30% of travel capacity.

Then, it verifies if it needs to optimize the route, if so, it just orders the planets in the index of the planet (that I saw it’s the order of them), and it will remove the duplicated planets.

And the last step it the space travel itself. We are going through a while that verify if the spacecraft can travel, if so, we iterate through the planet list that we want to visit. In this process, we need to verify two things: if we have the distance capacity to travel between the actual planet and the planet that we need to go and if so, we need to check if we have capacity to come back to Earth (we don't need to be lost in space). With that, we can update the able to travel of the spacecraft.

And when we go through all possible planets, we just come back to Earth. The `SpaceTravelApplication` travel method return a Message result, that are creating using string builder, with some information about spacecraft, the travel capacity, the list of planets visited, if we satisfy our costumer with all the planets that he wants to go and the total distance that we travel.

#### The WPF Project

_Just a disclaimer: I never worked with WPF before (but I have some experience with MVVM), so I need to learn something and I didn't think it's all the best approach. If it's possible, I would do all the frontend in a webpage using such framework as React._

To use MVVM in the WPF project, I used the Caliburn.Micro Framework, using the documentation and some internet help. I built all the page in the `SpaceTravelView.xaml` and used the `SpaceTravelViewModel` for that screen. As it has all the logic to build the screen and the components itself.

To use the `.json` file, I come with a approach to create a class file that returns the json data. And with that I used the package `Newtonsoft.Json` to get the data, so I get the spacecraft and planets data. For that, I have a Model folder that has all the json property of the Spacecraft and for the Planet.

For the spacecraft component, I just build a dropdown with the spacecraft list and have a SpacecraftSelect object, in the dropdown component I just show the spacecraft name.

With the spacecraft, I have the passengers component, that it is just a slider with some labels on it. For the max label, I use the SpacecraftSelect object just showing the capacity, as it is to define the max slider value. For the currentPassengers value I just get the value of the Slider.

The planets components, it got me to work a little bit more. I come up with a solution to use a dropdown component and when I select a new value I just push this value to a new list and display it below my dropdown. _If I use React or a JS frontend, I can create a dropdown component that when I select a value, I can push it to a list and just display a Chip component inside my dropdown._

And then, I had a optimize checkbox for optimize the route and the route button itself. When I click the Travel button, it got the spacecraft and planets select and map it to my models in Logic project (because we didn't need all the props to calculate the route). So, it just calls the Logic project, calculate the travel route and send me back a result model to display the message result in the screen, with the planets visited and the total travel distance.

## Screenshots

Initial screen:
![image](https://user-images.githubusercontent.com/74380677/172510583-08c12544-741c-4538-9733-501da4774440.png)

Spacecraft selection:
![image](https://user-images.githubusercontent.com/74380677/172510610-5750d332-5ce3-4ebc-b2c5-e108506828f8.png)

Changin number of passengers:
![image](https://user-images.githubusercontent.com/74380677/172510635-c02d30c2-6ddb-485a-b26f-9675f9ebfffd.png)

Planets in a list to visit:
![image](https://user-images.githubusercontent.com/74380677/172510660-4b81feb9-6a30-45ee-8c7d-b2f768a2cf1e.png)

Travel with error and not optmize:
![image](https://user-images.githubusercontent.com/74380677/172510684-03991c91-5de8-44c0-9632-93b5690778a5.png)

Travel with error and optimize:
![image](https://user-images.githubusercontent.com/74380677/172510704-7f22dc7d-a4c7-4a7a-9696-e8bd7430b247.png)

Travel success not optmize:
![image](https://user-images.githubusercontent.com/74380677/172510760-7a0442f4-9ef1-42a7-b053-75dc5d404d87.png)

Travel success with optimize:
![image](https://user-images.githubusercontent.com/74380677/172510773-4d6c6aed-5808-435d-adad-7760412f7ccf.png)







