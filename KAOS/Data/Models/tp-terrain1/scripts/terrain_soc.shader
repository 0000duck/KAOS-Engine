//**********************************************************************
//
//	terrain_soc.shader for Q3 by Sims
//	Website : http://www.simonoc.com/
//
//**********************************************************************
//
// ======================================================================
// TERRAIN BLENDS
// ======================================================================
textures/terrain_soc/ter_dirtgravel
{
	qer_editorimage textures/terrain_soc/ter_dirtgravel.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	q3map_alphaMod dotproduct2 ( 0.0 0.0 0.75 )
	{
		// Primary
		map textures/terrain_soc/ter_dirt1.tga
		rgbGen identity
	}
	{
		// Secondary
		map textures/terrain_soc/ter_gravel1.tga
		blendFunc GL_SRC_ALPHA GL_ONE_MINUS_SRC_ALPHA
		rgbGen identity
		alphaGen vertex
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ter_dirtmud
{
	qer_editorimage textures/terrain_soc/ter_dirtmud.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	q3map_alphaMod dotproduct2 ( 0.0 0.0 0.75 )
	{
		// Primary
		map textures/terrain_soc/ter_dirt1.tga
		rgbGen identity
	}
	{
		// Secondary
		map textures/terrain_soc/ter_mud1.tga
		blendFunc GL_SRC_ALPHA GL_ONE_MINUS_SRC_ALPHA
		rgbGen identity
		alphaGen vertex
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ter_gravelmud
{
	qer_editorimage textures/terrain_soc/ter_gravelmud.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	q3map_alphaMod dotproduct2 ( 0.0 0.0 0.75 )
	{
		// Primary
		map textures/terrain_soc/ter_gravel1.tga
		rgbGen identity
	}
	{
		// Secondary
		map textures/terrain_soc/ter_mud1.tga
		blendFunc GL_SRC_ALPHA GL_ONE_MINUS_SRC_ALPHA
		rgbGen identity
		alphaGen vertex
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ter_gravelsand
{
	qer_editorimage textures/terrain_soc/ter_gravelsand.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	q3map_alphaMod dotproduct2 ( 0.0 0.0 0.75 )
	{
		// Primary
		map textures/terrain_soc/ter_gravel1.tga
		rgbGen identity
	}
	{
		// Secondary
		map textures/terrain_soc/ter_sand1.tga
		blendFunc GL_SRC_ALPHA GL_ONE_MINUS_SRC_ALPHA
		rgbGen identity
		alphaGen vertex
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ter_mossgravel
{
	qer_editorimage textures/terrain_soc/ter_mossgravel.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	q3map_alphaMod dotproduct2 ( 0.0 0.0 0.75 )
	{
		// Primary
		map textures/terrain_soc/ter_moss1.tga
		rgbGen identity
	}
	{
		// Secondary
		map textures/terrain_soc/ter_gravel1.tga
		blendFunc GL_SRC_ALPHA GL_ONE_MINUS_SRC_ALPHA
		rgbGen identity
		alphaGen vertex
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ter_mossmud
{
	qer_editorimage textures/terrain_soc/ter_mossmud.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	q3map_alphaMod dotproduct2 ( 0.0 0.0 0.75 )
	{
		// Primary
		map textures/terrain_soc/ter_moss1.tga
		rgbGen identity
	}
	{
		// Secondary
		map textures/terrain_soc/ter_mud1.tga
		blendFunc GL_SRC_ALPHA GL_ONE_MINUS_SRC_ALPHA
		rgbGen identity
		alphaGen vertex
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ter_mudgravel
{
	qer_editorimage textures/terrain_soc/ter_mudgravel.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	q3map_alphaMod dotproduct2 ( 0.0 0.0 0.75 )
	{
		// Primary
		map textures/terrain_soc/ter_mud1.tga
		rgbGen identity
	}
	{
		// Secondary
		map textures/terrain_soc/ter_gravel1.tga
		blendFunc GL_SRC_ALPHA GL_ONE_MINUS_SRC_ALPHA
		rgbGen identity
		alphaGen vertex
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ter_rockgravel
{
	qer_editorimage textures/terrain_soc/ter_rockgravel.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	q3map_alphaMod dotproduct2 ( 0.0 0.0 0.75 )
	{
		// Primary
		map textures/terrain_soc/rock_grey3.tga
		rgbGen identity
	}
	{
		// Secondary
		map textures/terrain_soc/ter_gravel1.tga
		blendFunc GL_SRC_ALPHA GL_ONE_MINUS_SRC_ALPHA
		rgbGen identity
		alphaGen vertex
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ter_rockmud
{
	qer_editorimage textures/terrain_soc/ter_rockmud.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	q3map_alphaMod dotproduct2 ( 0.0 0.0 0.75 )
	{
		// Primary
		map textures/terrain_soc/rock_grey3.tga
		rgbGen identity
	}
	{
		// Secondary
		map textures/terrain_soc/ter_mud1.tga
		blendFunc GL_SRC_ALPHA GL_ONE_MINUS_SRC_ALPHA
		rgbGen identity
		alphaGen vertex
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ter_rocksand
{
	qer_editorimage textures/terrain_soc/ter_rocksand.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	q3map_alphaMod dotproduct2 ( 0.0 0.0 0.75 )
	{
		// Primary
		map textures/terrain_soc/rock_grey3.tga
		rgbGen identity
	}
	{
		// Secondary
		map textures/terrain_soc/ter_sand1.tga
		blendFunc GL_SRC_ALPHA GL_ONE_MINUS_SRC_ALPHA
		rgbGen identity
		alphaGen vertex
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ter_sandgravel
{
	qer_editorimage textures/terrain_soc/ter_sandgravel.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	q3map_alphaMod dotproduct2 ( 0.0 0.0 0.75 )
	{
		// Primary
		map textures/terrain_soc/ter_sand1.tga
		rgbGen identity
	}
	{
		// Secondary
		map textures/terrain_soc/ter_gravel1.tga
		blendFunc GL_SRC_ALPHA GL_ONE_MINUS_SRC_ALPHA
		rgbGen identity
		alphaGen vertex
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}

// ======================================================================
// VECTOR ALIGNMENT
// ======================================================================
textures/terrain_soc/ivec_dirt1
{
	qer_editorimage textures/terrain_soc/ivec_dirt1.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	{
		map textures/terrain_soc/ter_dirt1.tga
		rgbGen identity
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ivec_gravel1
{
	qer_editorimage textures/terrain_soc/ivec_gravel1.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	{
		map textures/terrain_soc/ter_gravel1.tga
		rgbGen identity
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ivec_mud1
{
	qer_editorimage textures/terrain_soc/ivec_mud1.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	{
		map textures/terrain_soc/ter_mud1.tga
		rgbGen identity
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ivec_moss1
{
	qer_editorimage textures/terrain_soc/ivec_moss1.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	{
		map textures/terrain_soc/ter_moss1.tga
		rgbGen identity
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ivec_rock3
{
	qer_editorimage textures/terrain_soc/ivec_rock3.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	{
		map textures/terrain_soc/rock_grey3.tga
		rgbGen identity
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ivec_sand1
{
	qer_editorimage textures/terrain_soc/ivec_sand1.tga
	q3map_tcGen ivector ( 256 0 0 ) ( 0 256 0 )
	{
		map textures/terrain_soc/ter_sand1.tga
		rgbGen identity
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ======================================================================
// GREY ROCK BLENDS (VINE and MOSS1)
// ======================================================================
textures/terrain_soc/rock_grey2
{
	qer_editorimage textures/terrain_soc/rock_grey2.tga
	
	q3map_nonplanar
	q3map_shadeAngle 75
	
	{
		map $lightmap
		rgbGen identity
	}
	{
		map textures/terrain_soc/rock_grey2.tga
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/rock_grey3
{
	qer_editorimage textures/terrain_soc/rock_grey3.tga
	
	q3map_nonplanar
	q3map_shadeAngle 75
	
	{
		map $lightmap
		rgbGen identity
	}
	{
		map textures/terrain_soc/rock_grey3.tga
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/ter_moss1wall
{
	qer_editorimage textures/terrain_soc/ter_moss1wall.tga
	
	q3map_nonplanar
	q3map_shadeAngle 75
	
	{
		map $lightmap
		rgbGen identity
	}
	{
		map textures/terrain_soc/ter_moss1wall.tga
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/rock_grey2moss1
{
	qer_editorimage textures/terrain_soc/rock_grey2moss1.tga
	
	q3map_nonplanar
	q3map_shadeAngle 75
	
	{
		// Primary
		map textures/terrain_soc/rock_grey2.tga
		rgbGen identity
	}
	{
		// Secondary
		map textures/terrain_soc/ter_moss1wall.tga
		blendFunc GL_SRC_ALPHA GL_ONE_MINUS_SRC_ALPHA
		rgbGen identity
		alphaGen vertex
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/rock_grey2vine
{
	qer_editorimage textures/terrain_soc/rock_grey2vine.tga
	
	q3map_nonplanar
	q3map_shadeAngle 75
	
	{
		map $lightmap
		rgbGen identity
	}
	{
		map textures/terrain_soc/rock_grey2vine.tga
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/rock_grey2vine_blend
{
	qer_editorimage textures/terrain_soc/rock_grey2vine_blend.tga
	
	q3map_nonplanar
	q3map_shadeAngle 75
	
	{
		// Primary
		map textures/terrain_soc/rock_grey2.tga
		rgbGen identity
	}
	{
		// Secondary
		map textures/terrain_soc/rock_grey2vine.tga
		blendFunc GL_SRC_ALPHA GL_ONE_MINUS_SRC_ALPHA
		alphaFunc GE128
		rgbGen identity
		alphaGen vertex
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ======================================================================
// BROWN ROCK BLENDS (VINE and MOSS1)
// ======================================================================
textures/terrain_soc/rock_brown2
{
	qer_editorimage textures/terrain_soc/rock_brown2.tga
	
	q3map_nonplanar
	q3map_shadeAngle 75
	
	{
		map $lightmap
		rgbGen identity
	}
	{
		map textures/terrain_soc/rock_brown2.tga
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/rock_brown3
{
	qer_editorimage textures/terrain_soc/rock_brown3.tga
	
	q3map_nonplanar
	q3map_shadeAngle 75
	
	{
		map $lightmap
		rgbGen identity
	}
	{
		map textures/terrain_soc/rock_brown3.tga
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/rock_brown2vine
{
	qer_editorimage textures/terrain_soc/rock_brown2vine.tga
	
	q3map_nonplanar
	q3map_shadeAngle 75
	
	{
		map $lightmap
		rgbGen identity
	}
	{
		map textures/terrain_soc/rock_brown2vine.tga
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/rock_brown2vine_blend
{
	qer_editorimage textures/terrain_soc/rock_brown2vine_blend.tga
	
	q3map_nonplanar
	q3map_shadeAngle 75
	
	{
		// Primary
		map textures/terrain_soc/rock_brown2.tga
		rgbGen identity
	}
	{
		// Secondary
		map textures/terrain_soc/rock_brown2vine.tga
		blendFunc GL_SRC_ALPHA GL_ONE_MINUS_SRC_ALPHA
		alphaFunc GE128	// hard edge blend
		rgbGen identity
		alphaGen vertex
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ----------------------------------------------------------------------
textures/terrain_soc/rock_brown2moss1
{
	qer_editorimage textures/terrain_soc/rock_brown2moss1.tga
	
	q3map_nonplanar
	q3map_shadeAngle 75
	
	{
		// Primary
		map textures/terrain_soc/rock_brown2.tga
		rgbGen identity
	}
	{
		// Secondary
		map textures/terrain_soc/ter_moss1wall.tga
		blendFunc GL_SRC_ALPHA GL_ONE_MINUS_SRC_ALPHA
		rgbGen identity
		alphaGen vertex
	}
	{
		map $lightmap
		blendFunc GL_DST_COLOR GL_ZERO
		rgbGen identity
	}
}
// ======================================================================
// BLACK SKY
// ======================================================================
textures/terrain_soc/sky_black
{
	qer_editorimage textures/terrain_soc/black.jpg

	//red green blue intensity degrees elevation deviance samples
	q3map_sunExt 1 1 .93 125 270 50 2 32
	q3map_skyLight 125 6

	q3map_noFog
	q3map_globalTexture
	surfaceparm sky
	surfaceparm noimpact
	surfaceparm nolightmap
	nopicmip
	{
		map textures/terrain_soc/black.tga
	}
}
// ======================================================================
// ALPHA BRUSHES
// ======================================================================
textures/terrain_soc/alpha_000
{
	qer_editorimage textures/terrain_soc/alpha_000.tga
	q3map_alphaMod volume
	q3map_alphaMod set 0.0
	surfaceparm nodraw
	surfaceparm nonsolid
	surfaceparm trans
	qer_trans 0.7
}
textures/terrain_soc/alpha_025
{
	qer_editorimage textures/terrain_soc/alpha_025.tga
	q3map_alphaMod volume
	q3map_alphaMod set 0.25
	surfaceparm nodraw
	surfaceparm nonsolid
	surfaceparm trans
	qer_trans 0.7
}
textures/terrain_soc/alpha_050
{
	qer_editorimage textures/terrain_soc/alpha_050.tga
	q3map_alphaMod volume
	q3map_alphaMod set 0.5
	surfaceparm nodraw
	surfaceparm nonsolid
	surfaceparm trans
	qer_trans 0.7
}
textures/terrain_soc/alpha_075
{
	qer_editorimage textures/terrain_soc/alpha_075.tga
	q3map_alphaMod volume
	q3map_alphaMod set 0.75
	surfaceparm nodraw
	surfaceparm nonsolid
	surfaceparm trans
	qer_trans 0.7
}
textures/terrain_soc/alpha_085
{
	qer_editorimage textures/terrain_soc/alpha_085.tga
	q3map_alphaMod volume
	q3map_alphaMod set 0.85
	surfaceparm nodraw
	surfaceparm nonsolid
	surfaceparm trans
	qer_trans 0.7
}
textures/terrain_soc/alpha_100
{
	qer_editorimage textures/terrain_soc/alpha_100.tga
	q3map_alphaMod volume
	q3map_alphaMod set 1.0
	surfaceparm nodraw
	surfaceparm nonsolid
	surfaceparm trans
	qer_trans 0.7
}
