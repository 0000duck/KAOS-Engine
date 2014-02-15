#version 410 core
 
 out VS_OUT
 {
	vec3 tc;
 }vs_out;

uniform vec3 eye_position;
uniform mat4 view_matrix;

in lowp vec3 in_position;

 void main(void)
 {
	vs_out.tc = mat3(view_matrix) * in_position.xyz;
	gl_Position = vec4(in_position.xyz - eye_position, 1.0);
 }