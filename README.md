# Jumper Assignment Lina Tabla

# Inhoud
1. [Introductie](#introductie)
2. [Nodige software en voorinstallatie](#benodigdheden)
3. [Spelverloop](#spelverloop)
4. [De spelomgeving](#spelomgeving)
    - [Speelveld object](#speelveldobject)
    - [Player object](#playerobject)
    - [Obstacle object](#obstacleobject)
5. [Spelobjecten scripts (C#)](#allescripts)
    - [Spawner.cs (omgeving) *code-snippets*](#scripts)
        * [Overzicht van de methodes](#spawner)
        * [Object-variabelen](#spawner2)
        * [Initialisatie](#spawner3)
        * [Opkuisen van het speelveld](#spawner4)
        * [Scorebord](#environment5)
        * [Genereren van een traveller (reiziger)](#environment6)
    - [Traveller.cs (reiziger) *code-snippets*](#scripts2)        
    - [Thief.cs (dief) *code-snippets*](#scripts3)
        * [Overzicht van methodes](#thief)
        * [Object variabelen](#thief2)
        * [Initialiseer de dief](#thief3)
        * [OnEpisodeBegin](#thief4)
        * [Heuristic](#thief5)
        * [OnActionReceived](#thief6)
        * [OnCollisionEnter](#thief7)
        * [DestroyObjects (Optimizations)](#thief8)
6. [Observaties, acties & beloning systeem](#beloning)
7. [Resultaat in Tensorflow](#tensorflow)

## Introductie <a name="introductie"></a>
In  deze tutorial zullen we stap voor stap uitleggen hoe je door middel van *Machine Learning* - *ML Agents*, het gebruik van *Unity3D* en met behulp van *C#* code een basic project tot stand kan brengen. Iemand zonder programmeer ervaring zou deze tutorial ook moeten kunnen volgen. 

## Nodige software en voorinstallatie <a name="benodigdheden"></a>
### Software
- [C# Visual Studio](https://visualstudio.microsoft.com/downloads/)
- [Python 3](https://www.python.org/downloads/)
- [Unity 3D](https://unity3d.com/get-unity/download)
- [Anaconda](https://docs.anaconda.com/anaconda/)

### Voorinstallatie
1. Start een nieuw Unity 3D project
2. Via de Package Manager van Unity installeer je ML Agents (download & import)

## Spelverloop <a name="spelverloop"></a>
In dit hoofdstuk wordt het spelverloop kort beschreven. De twee belangrijkste objecten in het spel zijn: de **agent (Player)** en een **obstakel (Obstacle)**. 
De zelflerende agent ontwijkt het obstakel door er over te springen. Het obstakel krijgt elke episode een andere snelheid mee. 
De agent noemen we *zelflerend* omdat die door middel van Machine Learning gaat leren om zo snel mogelijk onder de knie te krijgen wanneer een obstakel passeert op zijn pad om vervolgens te springen over het obstakel. Als dit succesvol verloopt, dan zal de Agent **beloont** worden.

## De spelomgeving <a name="spelomgeving"></a>
We starten met alle objecten te creëren. Er zijn 3 objecten: Road, Player en Obstacle.
<br>
Onderstaande afbeelding toont de volledige hiërarchie binnen de spelobjecten met hun benaming zoals ze in deze handleiding gebruikt zullen worden.
<br>
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/hi%C3%ABrachie-objecten.png"/>
<br>
<br>

### Speelveld object <a name="speelveldobject"></a>
Het speelveld heeft volgende eigenschappen:
* Naam: *Road*
* 3D Object: Cube
* Positie en rotatie: X = Y = Z = 0
* Schaal: X = 2 | Y = 0.1 | Z = 10
* Tag: road

<br>
Selecteer het Road object in Unity en voeg volgend component eraan toe:
<br>
<br>

**Box Collider**
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/boxcollider.png"/>
<br>
>Zorg ervoor dat de instellingen van het component *Box Collider* helemaal hetzelfde zijn als de afbeelding hierboven.
<br>

#### Speelveld: child-objecten
Het Road object heeft vier child-objecten, nl. SpawnPoint, WallEnd, Reset en WallTop. We gaan elk child-object toevoegen:
<br>

##### SpawnPoint
Voeg aan het *Road* object een *Empty* object toe en geef het volgende eigenschappen:
* Naam: SpawnPoint
* Positie: X = 0 | Y = 4 | Z = 0.3
* Rotatie: X = Y = Z = 0
* Scale: X = 0.5 | Y = 0.5 | Z = 0.5
* Eventueel een roze *Icon* zodat het goed te zien is

<br>

##### WallEnd
Voeg aan het *Road* object een *Cube* object toe en geef het volgende eigenschappen:
- Naam: WallEnd
- Positie: X = 0 | Y = 4.14 | Z = -0.55
- Rotatie: X = Y = Z = 0
- Scale: X = 1.2 | Y = 12 | Z = 0.12
- Tag: wallend

<br>
Selecteer het *Wallend* object in Unity en voeg volgend component eraan toe:
<br>
<br>

**Box Collider**
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/boxcollider.png"/>
<br>
>Zorg ervoor dat de instellingen van het component *Box Collider* helemaal hetzelfde zijn als de afbeelding hierboven.
<br>

##### Reset
Voeg aan het *Road* object een *Empty* object toe en geef het volgende eigenschappen:
* Naam: Reset
* Positie: X = 0 | Y = 3.5 | Z = -0.45
* Rotatie: X = Y = Z = 0
* Scale: X = 0.5 | Y = 0.5 | Z = 0.5
* Eventueel een groene *Icon* zodat het goed te zien is

<br>

##### WallTop
Voeg aan het *Road* object een *Cube* object toe en geef het volgende eigenschappen:
* Naam: WallTop
* Positie: X = -0.003 | Y = 31.3 | Z = -0.389
* Rotatie: X = Y = Z = 0
* Scale: X = 1 | Y = 4 | Z = 0.15
* Tag: walltop

<br>
Selecteer het *WallTop* object in Unity en voeg volgend component eraan toe:
<br>
<br>

**Box Collider**
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/boxcollider.png"/>
<br>
>Zorg ervoor dat de instellingen van het component *Box Collider* helemaal hetzelfde zijn als de afbeelding hierboven.
<br>


### Player object <a name="playerobject"></a>
De player heeft volgende eigenschappen:
- Naam: *Player*
- 3D Object: Cube
- Schaal: X = 0.5 | Y = 0.5 | Z = 0.5
- Positie: X = 0 | Y = 0.35 | Z = -4.5
- Rotatie: X = 0 | Y = 0 | Z = 90

<br>
Selecteer de Player object in Unity en voeg volgende componenten eraan toe:
<br>

**Rigidbody**
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/rigidbody.png"/>
<br>
>Zorg ervoor dat de instellingen van het component *Rigibody* helemaal hetzelfde zijn als de afbeelding hierboven.
<br>

**Box Collider**
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/boxcollider.png"/>
<br>
>Zorg ervoor dat de instellingen van het component *Box Collider* helemaal hetzelfde zijn als de afbeelding hierboven.
<br>

**Ray Perception Sensor3D**
<br>
De acties die de dief object zal gaan uitvoeren zijn gebaseerd op observaties. Om ervoor te gaan zorgen dat de dief kan gaan observeren maken we gebruiken van het **Ray Perception Sensor3D** component.
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/rps3D.png"/>
<br>
>Zorg ervoor dat de instellingen van het component *Ray Perception Sensor3D* helemaal hetzelfde zijn als de afbeelding hierboven.
<br>

**Behavior Parameters**
<br>
Voeg een *Behavior parameters* toe met de naam **Player**
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/behaviorparam.png"/>
<br>
>Zorg ervoor dat de instellingen van het component *Behavior Parameters* helemaal hetzelfde zijn als de afbeelding hierboven.
<br>

**Decision requester**
<br>
Voeg een *Decision requesters* toe (dit is een automatische trigger om de agent te dwingen iets te gaan doen)
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/decisionrequester.png"/>
<br>
>Zorg ervoor dat de instellingen van het component *Decision requester* helemaal hetzelfde zijn als de afbeelding hierboven.
<br>


### Obstacle object <a name="obstacleobject"></a>
Het obstakel heeft volgende eigenschappen:
- Naam: *Obstacle*
- 3D Object: Cube
- Positie: X = 0 | Y = 0.4 | Z = 3
- Rotatie: X = Y = Z = 0
- Schaal: X = 1 | Y = 0.5 | Z = 0.5
- Tag: obstacle

<br>
Selecteer het Obstacle object in Unity en voeg volgende componenten eraan toe:
<br>
<br>

**Rigidbody**
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/rigidbody.png"/>
<br>
>Zorg ervoor dat de instellingen van het component *Rigibody* helemaal hetzelfde zijn als de afbeelding hierboven.
<br>

**Box Collider**
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/boxcollider.png"/>
<br>
>Zorg ervoor dat de instellingen van het component *Box Collider* helemaal hetzelfde zijn als de afbeelding hierboven.
<br>

#### Obstacle: child-objecten
Het Obstacle object heef één child-object, nl. WallReward. We gaan dit child-object toevoegen:
<br>

##### WallReward
Voeg aan het *Obstacle* object een *Cube* object toe en geef het volgende eigenschappen:
- Naam: WallReward
- Positie: X = 0 | Y = 2.14 | Z = 1.12
- Rotatie: X = Y = Z = 0
- Scale: X = 1 | Y = 5 | Z = 0.5
- Tag: wallreward

<br>
Selecteer het WallReward object in Unity en voeg volgende componenten eraan toe:
<br>
<br>

**Rigidbody**
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/rigidbody.png"/>
<br>
>Zorg ervoor dat de instellingen van het component *Rigibody* helemaal hetzelfde zijn als de afbeelding hierboven.
<br>

**Box Collider**
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/boxcollider.png"/>
<br>
>Zorg ervoor dat de instellingen van het component *Box Collider* helemaal hetzelfde zijn als de afbeelding hierboven.
<br>

#### Obstacle: prefab
Maak van het Obstacle object een *Prefab*
<br>


## Spelobjecten scripts (C#) <a name="allescripts"></a>
Hieronder zullen we stapsgewijs per 3D object bekijken wat er moet staan in de script bestanden om ze een bepaald *gedrag* te geven en bepaalde *handelingen* te kunnen laten uitvoeren bij bepaalde *omstandigheden*.
<br>
Maak een nieuw folder aan in de project folder genaamd Scripts. Hierin zullen alle script bestanden staan die we gaan creëren en zullen gebruiken op het juiste 3D object.

### Spawner.cs <a name="scripts"></a>
Het eerste script bestandje die we zullen maken krijgt de naam *Spawner*. In het *Spawner.cs* script staat er code in om de **Obstacle** te doen spawnen op het platform. Al deze handelingen zullen bij het runnen van het project automatisch worden uitgevoerd door Unity.

**Overzicht van de methodes** <a name="spawner"></a>
In de ``Spawner Class`` zullen we volgende methodes gaan aanmaken.


[Spawner.cs (omgeving) *code-snippets*](#scripts)
        * [Overzicht van de methodes](#spawner)
        * [Object-variabelen](#spawner2)
        * [Initialisatie](#spawner3)
        * [Opkuisen van het speelveld](#spawner4)
        * [Scorebord](#environment5)
        * [Genereren van een traveller (reiziger)](#environment6)