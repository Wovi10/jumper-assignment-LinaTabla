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
        * [Script koppelen](#spawner3)
    - [Player.cs *code-snippets*](#scripts2)
        * [Overzicht van de methodes](#player)
        * [Object-variabelen](#player2)
        * [Script koppelen](#player3)        
    - [Obstacle.cs *code-snippets*](#scripts3)
        * [Overzicht van methodes](#obstacle)
        * [Object variabelen](#obstacle2)
        * [Script koppelen](obstacle3)    
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

Selecteer het Road object in Unity en voeg volgend component eraan toe:
<br>
<br>

**Box Collider**
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/boxcollider.png"/>
<br>

>Zorg ervoor dat de instellingen van het component *Box Collider* helemaal hetzelfde zijn als de afbeelding hierboven.
<br>

**Child-objecten**
<br>
Het Road object heeft vier child-objecten, nl.
* SpawnPoint
* WallEnd
* Reset
* WallTop

<br>

We gaan elk child-object toevoegen:
<br>

#### SpawnPoint
Voeg aan het *Road* object een *Empty* object toe en geef het volgende eigenschappen:
* Naam: *SpawnPoint*
* Positie: X = 0 | Y = 4 | Z = 0.3
* Rotatie: X = Y = Z = 0
* Scale: X = 0.5 | Y = 0.5 | Z = 0.5
* Eventueel een roze *Icon* zodat het goed te zien is

#### WallEnd
Voeg aan het *Road* object een *Cube* object toe en geef het volgende eigenschappen:
- Naam: *WallEnd*
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

#### Reset
Voeg aan het *Road* object een *Empty* object toe en geef het volgende eigenschappen:
* Naam: Reset
* Positie: X = 0 | Y = 3.5 | Z = -0.45
* Rotatie: X = Y = Z = 0
* Scale: X = 0.5 | Y = 0.5 | Z = 0.5
* Eventueel een groene *Icon* zodat het goed te zien is

<br>

#### WallTop
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

**Ray Perception Sensor 3D**
<br>
De acties die de dief object zal gaan uitvoeren zijn gebaseerd op observaties. Om ervoor te gaan zorgen dat de dief kan gaan observeren maken we gebruiken van het **Ray Perception Sensor 3D** component.
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


**Child-objecten**
<br>

Het Obstacle object heef één child-object, nl. *WallReward*. We gaan dit child-object toevoegen:
<br>

#### WallReward
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


**Obstacle: prefab**
<br>

Maak van het Obstacle object een *Prefab*
<br>
<br>


## Spelobjecten scripts (C#) <a name="allescripts"></a>
Hieronder zullen we stapsgewijs per 3D object bekijken wat er moet staan in de script bestanden om ze een bepaald *gedrag* te geven en bepaalde *handelingen* te kunnen laten uitvoeren bij bepaalde *omstandigheden*.
<br>
Maak een nieuw folder aan in de project folder genaamd Scripts. Hierin zullen alle script bestanden staan die we gaan creëren en zullen gebruiken op het juiste 3D object.

### Spawner.cs <a name="scripts"></a>
Het eerste script bestandje die we zullen maken krijgt de naam *Spawner*. In het *Spawner.cs* script staat er code in om de **Obstacle** te doen spawnen op het platform. Al deze handelingen zullen bij het runnen van het project automatisch worden uitgevoerd door Unity.

**Overzicht van de methodes** <a name="spawner"></a>
<br>
In de ``Spawner Class`` zullen we volgende methodes gaan aanmaken:
- ``InvokeRepeating`` -> Is een functie die ervoor zal zorgen dat de ``Spawn()`` methode op een bepaald tijdstip terug zal worden aangeroepen.

- ``Spawn`` -> Deze methode zal ervoor zorgen dat de Obstacle zal worden gespawnt op het platform.

**Object-variabelen** <a name="spawner2"></a>
<br>
We creëren een aantal *Public* object-variabelen:
```csharp
public GameObject prefab = null;
public Transform spawnPoint = null;
public float min = 1.0f;
public float max = 3.5f;
```

**Script koppelen** <a name="spawner3"></a>
En nu kunnen we het ``Spawner`` script gaan koppelen met de *Obstacle* zoals op onderstaande afbeelding.
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/spawnerscript.png"/>
<br>

- We vullen de *Prefab* in met het *Obstacle* object
- We vullen het *Spawn Point* in met het *SpawnPoint* child-object van *Obstacle*


### Player.cs <a name="scripts2"></a>
In het Player.cs script bestand zal alle actie plaatsvinden. Want in dit script bestand zullen we van ons *Player* 3D object een Agent maken door de Player class te laten overerven van de Agent class binnenin C#.

```csharp
public class Player : Agent
{
}
```

**Overzicht van de methodes** <a name=player"></a>
<br>
In de ``Player Class`` zullen we volgende methodes gaan aanmaken:
- ``Initialize``
- ``OnActionReceived``
- ``Heuristic``
- ``OnEpisodeBegin``
- ``OnCollisionEnter``
- ``OnTriggerEnter``
- ``Thrust``
- ``ResetPlayer``

**Object-variabelen** <a name="player2"></a>
<br>
We creëren een aantal *public* en *private* object-variabelen:
```csharp
public float force = 15f;
public Transform reset = null;
private Rigidbody rb = null;
```

**Script koppelen** <a name="player3"></a>
En nu kunnen we het ``Player`` script gaan koppelen met de *Player* zoals op onderstaande afbeelding.
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/playerscript.png"/>
<br>

- We vullen de *Reset* in met het *Obstacle* object
- We vullen het *Reset* in met het *Reset* child-object van Road*


### Obstacle.cs <a name="scripts2"></a>
**Overzicht van de methodes** <a name=obstacle"></a>
<br>
In de ``Obstacle Class`` zullen we volgende methodes gaan aanmaken:
- ``Update``
- ``OnCollisionEnter``

**Object-variabelen** <a name="obstacle2"></a>
<br>
We creëren volgende *public* object-variabele:
```csharp
public float moveSpeed = 3.5f;
```

**Script koppelen** <a name="obstacle3"></a>
En nu kunnen we het ``Obstacle`` script gaan koppelen met de *Obstacle* zoals op onderstaande afbeelding.
<br>
<img alt="header-image" src="https://raw.githubusercontent.com/AP-IT-GH/jumper-assignment-LinaTabla/main/Images/obstaclescript.png"/>
<br>


## Resultaat in Tensorflow <a name="tensorflow"></a>
Als laatste stap bekijken we kort de trainingsfase. Maakt een bestand in de *root* folder van je project genaamd *learning*. Hier binnen in maakt u een **.yml** bestand aan met de als naam **Player** -> **Player.yml**. Binnenin deze file plakt u onderstaande instellingen.

```yaml
behaviors:
  PLayer:
    trainer_type: ppo
    max_steps: 5.0e7
    time_horizon: 64
    summary_freq: 10000
    keep_checkpoints: 5
    checkpoint_interval: 50000
    
    hyperparameters:
      batch_size: 32
      buffer_size: 9600
      learning_rate: 3.0e-4
      learning_rate_schedule: constant
      beta: 5.0e-3
      epsilon: 0.2
      lambd: 0.95
      num_epoch: 3
 
    network_settings:
      num_layers: 2
      hidden_units: 128
      normalize: false
      vis_encoder_type: simple
 
    reward_signals:
      extrinsic:
        strength: 1.0
        gamma: 0.99
      curiosity:
        strength: 0.02
        gamma: 0.99
        encoding_size: 256
        learning_rate : 1e-3
```
Zorg er ook voor dat de *Behavior Parameters* **Player** overeenkomt met die in de **.yml** bestand anders zal het niet werken.
<br>

Nu kun je je agent trainen en vervolgens de resultaten tonen in Tensorflow.