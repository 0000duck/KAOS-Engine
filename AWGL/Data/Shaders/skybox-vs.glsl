#version 410 core
 
 out VS_OUT
 {
	vec3 tc;
 }vs_out;

uniform vec3 eye_position;
uniform mat4 mv_matrix;
uniform mat4 mvp_matrix;

in lowp vec3 in_position;

 void main(void)
 {
	vs_out.tc = in_position;
	gl_Position = mvp_matrix * vec4(in_position, 1.0);
 }