import os
import re

# Configuration
DIRECTORIES = [
    r"c:\Users\desmo\Desktop\CSA-FullSourceCode\CSA-Clone-HTML\admin",
    r"c:\Users\desmo\Desktop\CSA-FullSourceCode\CSA-Clone-HTML\Scripts"
]
EXTENSIONS = [".html", ".js"]

# Search and Replace Map (case-insensitive search, case-preserved replacement where possible)
REPLACEMENTS = {
    r"CSA Academy": "iBelanja Academy",
    r"CSA Portal": "iBelanja Portal",
    r"CSA Admin": "iBelanja Admin",
    r"CSA": "iBelanja",
    r"csa.com": "ibelanja.com"  # For email/smtp domains
}

def replace_in_file(file_path):
    print(f"Processing: {file_path}")
    try:
        with open(file_path, 'r', encoding='utf-8') as f:
            content = f.read()
        
        original_content = content
        
        # Apply replacements using regex for case-insensitive matching but specific target patterns
        # We start with longer strings to avoid partial matches
        for pattern, replacement in REPLACEMENTS.items():
            content = re.sub(pattern, replacement, content, flags=re.IGNORECASE)
        
        if content != original_content:
            with open(file_path, 'w', encoding='utf-8') as f:
                f.write(content)
            print(f"  Updated: {file_path}")
        else:
            print(f"  No changes: {file_path}")
            
    except Exception as e:
        print(f"  Error processing {file_path}: {e}")

def main():
    for directory in DIRECTORIES:
        if not os.path.exists(directory):
            print(f"Directory not found: {directory}")
            continue
            
        for root, dirs, files in os.walk(directory):
            for file in files:
                if any(file.endswith(ext) for ext in EXTENSIONS):
                    file_path = os.path.join(root, file)
                    replace_in_file(file_path)

if __name__ == "__main__":
    main()
