namespace Sphere3D
{
	class BigPlanet : SphereGeometry3D
	{
		BigPlanet()
		{
			Radius = 30;
			Separators = 5;
		}
	}

	class PlanetRing : DiscGeometry3D
	{
		PlanetRing()
		{
			Radius = 70;
			Separators = 10;
		}
	}

	class SmallPlanet : SphereGeometry3D
	{
		SmallPlanet()
		{
			Radius = 5;
			Separators = 5;
		}
	}
}
