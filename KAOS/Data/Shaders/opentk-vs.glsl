#version 410 core

precision highp float;

// object space to camera space transformation
uniform mat4 modelview_matrix;
 
// camera space to clip coordinates
uniform mat4 projection_matrix;
 
// incoming vertex position
in vec3 in_position;
 
// incoming vertex normal
in vec3 in_normal;
 
// transformed vertex normal
out vec3 normal;

void main(void)
{
  //works only for orthogonal modelview
  //normal = (modelview_matrix * vec4(in_normal, 0)).xyz;
  
  gl_Position = projection_matrix * modelview_matrix * vec4(in_position, 1);
}