shader_type spatial;
render_mode cull_disabled;

uniform float WaveDensity = 1.25;
uniform float Speed = 2.0; 
uniform lowp vec3 BaseColor = vec3(0.4, 0.7, 1.69); // Base color
const lowp vec3 ColorFactor = vec3(0.0025, 0.025, 0.05); // How much the desired axis will affect color

void fragment()
{
	// Set the color
	  
	ALBEDO.r = BaseColor[0] + (-VERTEX.y) * ColorFactor[0];
	ALBEDO.g = BaseColor[1] + VERTEX.x * ColorFactor[1];
	ALBEDO.b = BaseColor[2] + VERTEX.y * ColorFactor[2];
 	
}

void vertex()
{
	lowp float val = cos(VERTEX.x * WaveDensity - TIME / Speed);

	VERTEX.y = val;
}