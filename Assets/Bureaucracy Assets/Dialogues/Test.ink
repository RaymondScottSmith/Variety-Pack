VAR name = ""
VAR species = ""
VAR favFood = ""
VAR intro = ""
VAR water = ""
VAR team = ""
VAR origin = ""

{intro}

-> main
=== main ===

+ [What was your name again?]
    My name is {name}.
    ->main
+ [What species are you again?]
    I am a {species}. Isn't that obvious?
    ->main
+ [What do you like to eat?]
    My diet is varied and complex. Generally it depends on where I am and what time of day it is. However, I'll never turn down some {favFood}.
    ->main
+ [Do you have any water type requirements?]
    I prefer {water} if at all possible. Anything else is swill.
    ->main
+ [What's your favorite sports team]
    {team} for life!
    ->main
+ [Where are you from?]
    I move around a lot, but I'm originally from {origin}.
    ->main
-> END
===function setName(newName)===
~name = newName
===function setSpecies(newSpecies)===
~species = newSpecies
===function setFood(newFood)===
~favFood = newFood
===function setIntro(newIntro)===
~intro = newIntro
===function setWater(newWater)===
~water = newWater
===function setTeam(newTeam)===
~team = newTeam
===function setOrigin(newOrigin)===
~origin = newOrigin