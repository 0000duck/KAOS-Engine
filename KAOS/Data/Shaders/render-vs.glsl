#version 410 core

uniform mat4 model_matrix;
uniform mat4 view_matrix;
uniform mat4 proj_matrix;


layout (location = 0) in vec4 in_position;
layout (location = 1) in vec3 in_normal;

out VS_OUT
{
    vec3 normal;
    vec3 view;
} vs_out;

void main(void)
{
	mat4  mv_matrix = view_matrix * model_matrix;
    vec4 pos_vs = mv_matrix * in_position;

    vs_out.normal = mat3(mv_matrix) * in_normal;
    vs_out.view = pos_vs.xyz;

    gl_Position = proj_matrix * pos_vs;
}