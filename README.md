# MapperSample #

## Use Case ##

We've used the Mapper in places where we're trying to convert multiple different sources of data to a common domain model. This happens most of the time when we are receiving input from vendor hardware or from external customer systems. In these cases, the input may be encoded specific to that vendor, the input may be incomplete, or the input may just need some TLC before it can be ingested into our systems. 

The Mapper allows us to have as much customization as neccessary to get it into the right format.

## Project Outline ##

* _Mapper_: Contains the main mapping logic. Each implementation gets its own subfolders. This is broken out by manufacturer in this particular example.
* _Model_: Contains the domain model that we're converting towards
* _Web_: Contains the web client which will call the Mappers for specific implementations and return the mapped results

## Sample Data Sources ##

In this sample project, we have two vendors whose hardware messages our system. The first is an HP digital press and the second is a Horizon inline finishing device. Both return messages with different content and different formats. 

When the messages are submitted, they are converted into the domain model with logic specific to the vendor's implementation.