#version 410 core
 
 out VS_OUT
 {
	vec3 tc;
 }vs_out;

uniform mat4 view_matrix;

in lowp vec3 in_position;

 void main(void)
 {
	vs_out.tc = mat3(view_matrix) * normalize(in_position.xyz);
	gl_Position = vec4(in_position.xyz, 1.0);
 }