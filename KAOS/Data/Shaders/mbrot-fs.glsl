#version 120

uniform sampler1D tex;
uniform vec2 center;
uniform float scale;
uniform int iter;

void main() {
    vec2 z, c;

    c.x = 1.3333 * (gl_TexCoord[0].x - 0.5) * scale - center.x;
    c.y = (gl_TexCoord[0].y - 0.5) * scale - center.y;

    int i;
    z = c;
    for(i=0; i<iter; i++) {
        float x = (z.x * z.x - z.y * z.y) + c.x;
        float y = (z.y * z.x + z.x * z.y) + c.y;

        if((x * x + y * y) > 4.0) break;
        z.x = x;
        z.y = y;
    }
	if (i == iter) {
		gl_FragColor = vec4(0.0, 0.0, 0.0, 0.0);
	} else {
		gl_FragColor = vec4(1.0, 0.0, 0.0, 0.0);
	}
}