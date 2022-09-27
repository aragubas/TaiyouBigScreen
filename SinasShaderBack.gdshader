shader_type spatial;
render_mode unshaded, cull_disabled;

uniform float WaveDensity = 1.25;
uniform float Speed = 2.0; 
uniform vec3 BaseColor = vec3(0.25, 0.25, 1.69);
const vec3 ColorFactor = vec3(0.132, 0.06, 1.245);
const vec3 GlowFactor = vec3(1.25, 1.25, 1.25);

void fragment()
{
	// Set the color	
	ALBEDO.r = BaseColor[0] * GlowFactor[0] + VERTEX.y * ColorFactor[0];
	ALBEDO.g = BaseColor[1] * GlowFactor[1] - (VERTEX.z * ColorFactor[1]);
	ALBEDO.b = BaseColor[2] * GlowFactor[2] + (VERTEX.y * ColorFactor[2]) * (NORMAL.y * 0.05);
}
 
void vertex()
{
	lowp float val = cos(VERTEX.x * WaveDensity - TIME / Speed);

	VERTEX.y = val;
}