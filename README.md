# SPACE TRAVEL APPLICATION


## Overview

A .Net desktop app, using WPF to alow someone to plan and visualize a space travel from earth, with a spacecraft.

When you got in the application, you need to select a spacecraft, in the dropdown, and the slider will update the max allow value to the capacity of the spacecraft. So, you can select a spacecraft and define the number of passengers that will travel. Note that the default value is one, because we don't want to the spacecraft travel alone 🙂.

## The code

In this section we are going throw the code it self.

### How to run the code

Just download or clone the repository folder, then you should open in a C# compiler that you like and run the WPF project. In my case I develop and run the component in the VisualStudio.

If you want, I'm attatching a .exe file in the root of the repository.

#### The Logic Project

It's just a project that will handle the logic of the space travel. It has a Interface folder for the SpaceTravelApplication, a Model folder that contains the models that are used in all calculation it self, you see there the Spacecraft, Planets and a Result model, to give us the result.

In the SpaceTravelApplication it contains the logic itself. First of all the program calculate the real travel distance, discounting the difference because the number of passengers, until it has the maximum capacity and lose 30% of travel capacity.

Then, it verify if it needs to optimze the route, if so, it just order the planets in the DISTANCE OF EARTH or INDEX.

And the last step it the space travel itself. We are going through a while thats verify the current capacity of travel, if we have some distance to travel, we iterate through the planet list that we want to visit. In this process, we need to verify two things: if we have the distace capacity to travel between the actual planet and the planet that we need to go and if so we need to check if we have capacity to come back to Earth (we dont need to be lost in space).

And when we go through all possible planets, we just come back to Earth. The SpaceTravelApplication returns the planet list that we visit, if we satisfy our costumer with all the planets that he wants to go and the total distance that we use.

#### The WPF Project

_Just a disclaimer: I never worked with WPF before (but I have some experience with MVVM), so I need to learn somethings and I didnt think its all the best approach. If its possible, I would do all the frontend in a webpage using such framework as React._

To use MVVM in the WPF project, I used the Caliburn Framework, using the documentation and some internet help. I built all the page in the SpaceTravel.xmls and used the ViewModel for that screen. As it has all the logic to build the screen and the components itself.

To use the `.json` file, I come with a approach to create a class file that returns the json data. And with that I used the package **JSONStringify** to get the data, so I get the spacecraft and planets data. For that, I have a Model folder that has all the json property of the Spacecraft and for the Planet.

For the spacecraft component, I just build a dropdown with the spacecraft list and have a SpacecraftSelect object, in the dropdown component I just show the spacecraft name.

With the spacecraft, I have the passengers component, that it is just a slider with some labels on it. For the max label, I use the SpacecraftSelect object just showing the capacity, as it is to define the max slider value. For the currentPassengers value I just get the value of the Slider.

The planets components it got me to work a little bit more. I come up with a solution to use a dropdown component and when I select a new value I just push this value to a new list and display it below my dropdown. _If I use React or a JS frontend, I can create a dropdown component that when I select a value, I can push it to a list and just display a Chip component inside my dropdown._

And then, I had a optimize checkbox for optimize the route and the route button itself. When I click the route button, it got the spacecraft and planets select and map it to my models in Logic project (because we didnt need all the props to calculate the route). So, it just call the Logic project, calculate the travel route and send me back a result model to display the result in the screen, with the planets visited and the total travel distance.
