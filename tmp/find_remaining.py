import os

root_dir = r'C:\Users\desmo\Desktop\CSA-FullSourceCode'
target_string = 'YAYASAN AMANAH BANTUAN AWAM MALAYSIA'

found_files = []
for root, dirs, files in os.walk(root_dir):
    if '.git' in dirs:
        dirs.remove('.git')
    for file in files:
        if file.endswith(('.html', '.js', '.ts', '.php', '.ejs', '.json')):
            filepath = os.path.join(root, file)
            try:
                with open(filepath, 'r', encoding='utf-8', errors='ignore') as f:
                    if target_string in f.read():
                        found_files.append(filepath)
            except Exception:
                pass

for f in found_files:
    print(f)
