#!/usr/bin/env python3
"""
Script to replace all black pixels with transparent pixels in PNG images.
Processes all PNG files in the dasa_prana_transparent folder.
"""

from PIL import Image
import os
from pathlib import Path

def convert_black_to_transparent(image_path):
    """
    Load a PNG image, replace all black pixels with transparent, and save it.
    
    Args:
        image_path: Path to the PNG image file
    """
    print(f"Processing: {image_path.name}")
    
    # Load image
    img = Image.open(image_path)
    
    # Convert to RGBA if not already
    if img.mode != 'RGBA':
        img = img.convert('RGBA')
    
    # Get pixel data
    pixels = img.load()
    width, height = img.size
    
    # Replace black pixels with transparent
    black_count = 0
    for y in range(height):
        for x in range(width):
            r, g, b, a = pixels[x, y]
            # Check if pixel is black (or very close to black)
            if r < 10 and g < 10 and b < 10:
                pixels[x, y] = (0, 0, 0, 0)  # Fully transparent
                black_count += 1
    
    # Save the image (overwrite original)
    img.save(image_path, 'PNG')
    print(f"  ✓ Converted {black_count:,} black pixels to transparent")
    print(f"  ✓ Saved: {image_path.name}")
    print()

def main():
    # Target directory with images to convert
    target_dir = Path("/Users/swag/Projects/Kathak Saangi/Assets/Images/dasa_prana_transparent")
    
    if not target_dir.exists():
        print(f"Error: Directory not found: {target_dir}")
        return
    
    # Get all PNG files
    png_files = list(target_dir.glob("*.png"))
    
    if not png_files:
        print(f"No PNG files found in: {target_dir}")
        return
    
    print(f"Found {len(png_files)} PNG files to process")
    print(f"Target directory: {target_dir}")
    print("=" * 60)
    print()
    
    # Process each PNG file
    for png_file in sorted(png_files):
        convert_black_to_transparent(png_file)
    
    print("=" * 60)
    print(f"✓ Complete! Processed {len(png_files)} images")
    print(f"Original images: {target_dir.parent / 'dasa_prana'}")
    print(f"Transparent images: {target_dir}")

if __name__ == "__main__":
    main()
