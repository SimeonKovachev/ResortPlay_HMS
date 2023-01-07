# Resort_Play

Accomodation Type
Id	Name
1	Hotel Room
2	Apartment


AccomodationPackage
Id	AccomodationTypeId	Name		NoOfRoom	PerNightFee
1	1			Standard	1		50
2	1			Deluxe		1		70
3	1			Suites		1		100
4	2			2 Bedroom	2		120
5	2			3 Bedroom	3		150


Accomodation
Id	AccomodationPackageId		RoomNo
1	1				190
2	1				191
3	2				192
4	1				193
5	3				194
6	2				195
7	4				Apartment No 230
8	4				Apartment No 231
9	5				Apartment No 207
10	4				Apartment No 253


Bookings
Id	AccomodationId		FromDate	Duration(No of Nights to stay)
1	3			22June2022	4
