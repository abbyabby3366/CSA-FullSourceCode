<%@ Page Title="" Language="C#" MasterPageFile="~/SiteExt.Master" AutoEventWireup="true" CodeBehind="Survey.aspx.cs" Inherits="csa.Member.Survey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2.min.css") %>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2-bootstrap-5-theme.min.css") %>" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css">
    <style>
        .sectiontitle {
            font-weight: 800;
            border-bottom: solid 1px silver;
            font-size: medium;
            width: 100%;
            margin-bottom: 10px;
            padding-top: 5px;
            display: block;
            color: gray;
        }
        .select2-container .select2-selection--multiple .select2-selection__choice {
            background-color: unset;
        }

        .select2-container--bootstrap-5 .select2-selection--multiple .select2-selection__rendered .select2-selection__choice {
            font-size: unset;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfModelView" runat="server" />
    <div class="auth-page-wrapper auth-bg-cover py-5 d-flex justify-content-center align-items-center min-vh-100">
        <div class="bg-overlay"></div>

        <!-- auth page content -->
        <div class="auth-page-content">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="text-center text-white-50">
                            <div>
                                <a href="javascript:void(0)" class="d-inline-block auth-logo">
                                    <img src="<%=Page.ResolveUrl("~/assets/images/logos/yabam-logo-dark.png") %>" alt="" height="100">
                                </a>
                            </div>
                            <%--<p class="mt-3 fs-15 fw-medium">Premium Admin & Dashboard Template</p>--%>
                        </div>
                    </div>
                </div>

                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="card mt-4">
                            <div class="card-body p-4" id="tab-1">
                                <p>
                                    <img src="images/yabam.jpg" alt="YABAM Program" style="width: 100%; height: auto;" />
                                </p>
                                    <label style="width:100%;"><input type="checkbox" data-group="terms" name="terms" data-error-target="#terms-error"/> <span>Saya bersetuju dengan <a href="TermsConditions.html" target="_blank">Terma Syarat Program Lestari Tinjauan</a></span><span id="terms-error"></span></label>
                                    <label style="width:100%;"><input type="checkbox" data-group="terms" name="terms-yabam" data-error-target="#terms-yabam-error"/> <span>Saya bersetuju dengan <a href="TermsConditionsYabam.html" target="_blank">Terms of Service (YABAM)</a></span> <span id="terms-yabam-error"></span></label>
                                    <label style="width:100%;"><input type="checkbox" data-group="terms" name="privacy-policy" data-error-target="#privacy-policy-error"/> <span>Saya bersetuju dengan <a href="PrivacyPolicy.html" target="_blank">NOTIS PRIVASI YABAM</a></span> <span id="privacy-policy-error"></span></label>
                                    <label style="width:100%;"><input type="checkbox" data-group="terms" name="cookies-policy" data-error-target="#cookies-policy-error"/> <span>Saya bersetuju dengan <a href="CookiePolicy.html" target="_blank">Cookies Policy</a></span> <span id="cookies-policy-error"></span></label>
                                    <label style="width:100%;"><input type="checkbox" name="terms-all"/> <span>Saya bersetuju dengan semua syarat disini</span></label>
                                                                                                
                                <div class="d-flex justify-content-center mt-4">
                                    <button class="btn btn-tertiary " type="button" id="btnNextBahagian1">Sertai</button>
                                </div>
                            </div>

                            <div class="card-body p-4 d-none" id="tab-2">
                                <div class="text-center mt-2">
                                    <h5 class="text-primary">Sila isi nama dan nombor telefon anda untuk mendaftar sebagai ahli YABAM dan memulakan tinjauan.</h5>
                                </div>

                                <div class="mt-3">
                                    <div class="d-flex flex-column">
                                        <label>Nombor Telefon</label>
                                        <div class="d-flex align-items-center gap-3">
                                            <span>+60</span>
                                            <input type="tel" class="form-control" name="accountPhone" />
                                        </div>
                                        <p class="text-muted">Use this format eg; 0123456789</p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6 col-sm-12 mt-2">
                                        <div class="d-flex flex-column">
                                            <label>Name Penuh (seperti IC)</label>
                                            <input type="text" class="form-control" name="accountFullname" />
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-sm-12 mt-2">
                                        <div class="d-flex flex-column">
                                            <label>Nama Rujukan</label>
                                            <input type="text" class="form-control" name="accountReferralName"/>
                                        </div>
                                    </div>
                                </div>



                                <div class="d-flex justify-content-end mt-4">
                                    <%--<button class="btn btn-tertiary d-none" type="button" id="btnBackBahagian2">Back</button>--%>
                                    <button class="btn btn-tertiary mx-2" type="button" id="btnNextBahagian2">Hantar</button>
                                </div>
                            </div>


                            <div class="card-body p-4 d-none" id="tab-3">
                                <div class="text-center mt-2">
                                    <h5 class="text-primary">PROGRAM LESTARI TINJAUAN</h5>
                                </div>

                                <span>Terima kasih kerana menyertai tinjauan ini untuk membantu kami mengenal pasti serta memahami cabaran kewangan yang dialami oleh golongan profesional di Malaysia. Sila merujuk kepada terma syarat berkenaan tinjauan ini serta prasyarat bagi mendapat ganjaran sehingga RM700.00. <a href="TermsConditions.html" target="_blank">Terma dan Syarat</a>
</span>
                                <div class="mt-2">
                                    <div class="d-flex flex-column">
                                        <label>1. Negeri manakah yang anda tinggal sekarang?</label>
                                        <select class="form-select" name="a1"></select>
                                    </div>
                                </div>
                                 <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>2. Apakah jawatan/ posisi anda di tempat kerja?</label>
                                        <div class="row">
                                            <div class="col-6 col-md-3">
                                                <select class="form-select nyatakan" name="a2" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>"></select>
                                                <div id="nyatakan_a2" class="d-none">
                                                    <label class="mt-2">Sila nyatakan jawatan anda</label>
                                                    <input type="text" class="form-control" name="a2a" />
                                                </div>
                                            </div>
                                            <div class="col-6 col-md-3">
                                                <select class="form-select nyatakan col-3" name="a2b" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>"></select>
                                                <div id="nyatakan_a2b" class="d-none">
                                                    <label class="mt-2">Sila nyatakan posisi anda</label>
                                                    <input type="text" class="form-control" name="a2c" />
                                                </div>
                                            </div>
                                        </div>
                                         
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>Nama Majikan</label>
                                        <input type="text" class="form-control" name="employerName" />
                                    </div>
                                </div>
                                <div class="mt-2">
                                    <div class="d-flex flex-column">
                                        <label>Jenis Majikan</label>
                                        <select class="form-select nyatakan" name="employerType" data-nyatakan="6">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">GOV/AN</option>
                                            <option value="2">GLC</option>
                                            <option value="3">MNC</option>
                                            <option value="4">Persendirian</option>
                                            <option value="5">Sijil Profesional</option>
                                            <option value="6">Lain-lain</option>
                                        </select>
                                         <div id="nyatakan_employerType" class="d-none">
                                            <label class="mt-2">Sila nyatakan</label>
                                            <input type="text" class="form-control" name="employerTypeOther" />
                                        </div>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>Status Pekerjaan</label>
                                        <select class="form-select nyatakan" name="employmentStatus" data-nyatakan="4">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">Tetap</option>
                                            <option value="2">Kontrak</option>
                                            <option value="3">Bekerja Sendiri</option>
                                            <option value="4">Lain-lain</option>
                                        </select>
                                        <div id="nyatakan_employmentStatus" class="d-none">
                                            <label class="mt-2">Sila nyatakan</label>
                                            <input type="text" class="form-control" name="employmentStatusOther" />
                                        </div>
                                    </div>
                                </div> 
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>Jumlah Tahun Berkhidmat dalam majikan sekarang</label>
                                        <select class="form-select" name="yearOfService">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">Kurang dari 1</option>
                                            <option value="2">1-2</option>
                                            <option value="3">3-5</option>
                                            <option value="4">Lebih dari 5</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>Umur Bersara</label>
                                        <input type="number" class="form-control" name="retirementAge" />
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>3. Apakah Taraf Pendidikan Anda?</label>
                                        <select class="form-select" name="a3">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">PMR</option>
                                            <option value="2">SPM</option>
                                            <option value="3">Diploma</option>
                                            <option value="4">Ijazah</option>
                                            <option value="5">Ijazah sarjana</option>
                                            <option value="6">PhD</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>4. Apakah status perkahwinan anda?</label>
                                        <select class="form-select" name="a4">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">Berkahwin</option>
                                            <option value="2">Bujang</option>
                                            <option value="3">Janda</option>
                                            <option value="4">Duda</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>5. Berapakah jumlah tanggungan anda termasuk diri sendiri?</label>
                                        <input type="number" class="form-control" name="a5" />
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>6. Adakah anda mempunyai tanggungan ahli keluarga OKU atau sakit tenat?</label>
                                        <select class="form-select" name="a6">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">Ya</option>
                                            <option value="2">Tidak</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>7. Adakah anda mempunyai rumah sendiri atau menyewa?</label>
                                        <select class="form-select" name="a7">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">Memiliki rumah sendiri</option>
                                            <option value="2">Menyewa</option>
                                            <option value="3">Rumah ibu bapa</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>8. Dimanakah lokasi kediaman rumah anda?</label>
                                        <select class="form-select" name="a8">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">Dalam Bandar</option>
                                            <option value="2">Luar Bandar</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>9. Berapakah jumlah kerata yang anda miliki?</label>
                                        <select class="form-select" name="a9">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">0</option>
                                            <option value="2">1</option>
                                            <option value="3">2</option>
                                            <option value="4">3 atau lebih</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>10. Apakah hobi/ minat anda ketika waktu lapang?</label>
                                        <select class="form-select nyatakan no-min-max-answer" name="a10" data-nyatakan="16" multiple="multiple" required>
                                            <option value="1">Menonton TV atau filem</option>
                                            <option value="2">Cafe hopping</option>
                                            <option value="3">Shopping atau online shopping</option>
                                            <option value="4">Karaoke dan muzik</option>
                                            <option value="5">Membaca atau menulis</option>
                                            <option value="6">Bersenam atau mengikuti kelas kecergasan</option>
                                            <option value="7">Melancong atau berkelah</option>
                                            <option value="8">Memasak</option>
                                            <option value="9">Kraf dan seni</option>
                                            <option value="10">Berkebun atau menjaga tanaman</option>
                                            <option value="11">Meluangkan masa untuk aktiviti keluarga</option>
                                            <option value="12">Berkunjung ke pasar malam atau bazar</option>
                                            <option value="13">Menyertai aktiviti sukarelawan di komuniti</option>
                                            <option value="14">Berlepak dengan rakan-rakan</option>
                                            <option value="15">Bermain permainan video atau mobile game</option>
                                            <option value="16">Lain-lain (sila nyatakan)</option>
                                        </select>
                                        <div id="nyatakan_a10" class="d-none">
                                            <label class="mt-2">Sila nyatakan</label>
                                            <input type="text" class="form-control" name="a10a" />
                                        </div>
                                    </div>
                                </div> 
                                <div class="d-flex justify-content-end mt-4">
                                    <button class="btn btn-tertiary " type="button" id="btnBackBahagian3">Kembali</button>
                                    <button class="btn btn-tertiary mx-2" type="button" id="btnNextBahagian3">Seterusnya</button>
                                </div>
                            </div>

                            <div class="card-body p-4 d-none" id="tab-4">
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>11. Berapakah jumlah pendapatan kasar sekeluarga anda?</label>
                                        <select class="form-select" name="b1">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">< RM3,000</option>
                                            <option value="2">RM3,001 - RM5,000</option>
                                            <option value="3">RM5,001 - RM10,000</option>
                                            <option value="4">RM10,001 - RM15,000</option>
                                            <option value="5">> RM15,000</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>12. Berapa banyak hutang anda buat masa sekarang? (tidak termasuk hutang rumah)</label>
                                        <select class="form-select" name="b2">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">< RM100,000</option>
                                            <option value="2">RM100,001 - RM200,000</option>
                                            <option value="3">> RM200,000</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>13. Berapakah simpanan anda sebulan?</label>
                                        <select class="form-select" name="b3">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">< RM400</option>
                                            <option value="2">RM401 - RM700</option>
                                            <option value="3">RM701 - RM1000</option>
                                            <option value="4">> RM1000</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>14. Apakah yang mendorong anda untuk menabung? (Sila pilih 3)</label>
                                        <select class="form-select" name="b4" multiple="multiple" data-min="3" data-max="3">
                                            <option value="1">Menyediakan dana kecemasan</option>
                                            <option value="2">Mencapai impian hidup (membeli kereta, percutian, perkahwinan)</option>
                                            <option value="3">Persediaan untuk persaraan</option>
                                            <option value="4">Menjamin masa depan pendidikan anak-anak</option>
                                            <option value="5">Mengurangkan beban hutang</option>
                                            <option value="6">Menghadapi ketidakpastian ekonomi</option>
                                            <option value="7">Sebagai modal perniagaan atau pelaburan</option>
                                            <option value="8">Menambah simpanan untuk perbelanjaan kesihatan</option>
                                            <option value="9">Menyediakan simpanan untuk keluarga atau tanggungan</option>
                                            <option value="10">Mencapai kestabilan kewangan dan ketenangan fikiran</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>15. Apakah cabaran paling sukar untuk menabung? (Silih pilih 3)</label>
                                        <select class="form-select nyatakan" name="b5" data-nyatakan="10" multiple="multiple" data-min="3" data-max="3">
                                            <option value="1">Kos sara hidup yang tinggi</option> 
                                            <option value="2">Pendapatan yang tidak mencukupi</option>
                                            <option value="3">Komitmen hutang yang tinggi (kad kredit, pinjaman)</option>
                                            <option value="4">Gaya hidup atau keperluan harian yang mahal</option>
                                            <option value="5">Kurang disiplin dalam mengawal perbelanjaan</option>
                                            <option value="6">Kecemasan atau perbelanjaan tidak dijangka</option>
                                            <option value="7">Keinginan untuk berbelanja lebih pada hiburan dan hobi</option>
                                            <option value="8">Sokongan kewangan kepada keluarga atau tanggungan</option>
                                            <option value="9">Kekurangan pengetahuan atau kemahiran pengurusan kewangan</option>
                                            <option value="10">Lain-lain (sila nyatakan)</option>
                                        </select>
                                        <div id="nyatakan_b5" class="d-none">
                                            <label class="mt-2">Sila nyatakan</label>
                                            <input type="text" class="form-control" name="b5a" />
                                        </div>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>16. Sepanjang tahun terkini, berapa kerap anda terpaksa meminjam wang atau menggunakan kredit untuk menampung perbelanjaan harian?</label>
                                        <select class="form-select" name="b6">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">Tidak pernah</option>
                                            <option value="2">Sekali atau dua kali</option>
                                            <option value="3">Beberapa kali</option>
                                            <option value="4">Secara berkala</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>17. Apakah ketakutan anda dalam menambah / menggunakan komitmen kewangan?</label>
                                        <select class="form-select" name="b7">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">Tidak mampu membayar balik hutang</option>                                            
                                            <option value="2">Beban kewangan yang bertambah dan menjejaskan gaya hidup</option>                                            
                                            <option value="3">Risiko muflis atau kehabisan wang simpanan</option>                                            
                                            <option value="4">Keperluan untuk mengorbankan matlamat kewangan lain (contohnya, pendidikan anak atau persaraan)</option>                                            
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>18. Jika anda menerima RM100,000 hari ini, bagaimana anda akan menggunakannya secara utama? (Sila pilih 3)</label>
                                        <select class="form-select nyatakan" name="b8" data-nyatakan="13" multiple="multiple" data-min="3" data-max="3">
                                            <option value="1">Membayar hutang peribadi (kad kredit, pinjaman)</option>                                                                                                                              
                                            <option value="2">Membayar pinjaman perumahan (gadai janji, pinjaman penambahbaikan rumah)</option>                                                                                                                              
                                            <option value="3">Melabur dalam saham, dana bersama, atau instrumen kewangan lain</option>                                                                                                                              
                                            <option value="4">Memulakan atau mengembangkan perniagaan</option>                                                                                                                              
                                            <option value="5">Menyimpan untuk pendidikan anak-anak</option>                                                                                                                              
                                            <option value="6">Membeli atau memperbaiki rumah</option>                                                                                                                              
                                            <option value="7">Membina tabung kecemasan</option>                                                                                                                              
                                            <option value="8">Melancong bersama keluarga atau aktiviti rekreasi</option>                                                                                                                              
                                            <option value="9">Membeli kereta baru atau barangan rumah utama</option>                                                                                                                              
                                            <option value="10">Menampung perbelanjaan perubatan atau keperluan berkaitan kesihatan</option>                                                                                                                              
                                            <option value="11">Menderma atau menyokong badan amal</option>                                                                                                                              
                                            <option value="12">Menyimpan untuk persaraan</option>                                                                                                                              
                                            <option value="13">Lain-lain (sila nyatakan)</option>                                                                                                                              
                                        </select>
                                        <div id="nyatakan_b8" class="d-none">
                                            <label class="mt-2">Sila nyatakan</label>
                                            <input type="text" class="form-control" name="b8a" />
                                        </div>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>19. Apakah jenis perniagaan atau pelaburan di pandangan anda mampu jana pendapatan tinggi? (Silih pilih 3)</label>
                                        <select class="form-select nyatakan" name="b9" data-nyatakan="10" multiple="multiple" data-min="3" data-max="3">
                                            <option value="1">Hartanah</option>                                                                                                                              
                                            <option value="2">Emas</option>                                                                                                                              
                                            <option value="3">Saham atau pasaran modal</option>                                                                                                                              
                                            <option value="4">Forex atau Crypto</option>                                                                                                                              
                                            <option value="5">Trust Fund atau pelaburan amanah</option>                                                                                                                              
                                            <option value="6">Perniagaan F&B (makanan dan minuman)</option>                                                                                                                              
                                            <option value="7">Perniagaan E-Dagang</option>                                                                                                                              
                                            <option value="8">Perniagaan kecantikan dan kesihatan</option>                                                                                                                              
                                            <option value="9">MLM atau Direct Sales</option>                                                                                                                              
                                            <option value="10">Lain-lain (sila nyatakan)</option>                                                                                                                              
                                        </select>
                                        <div id="nyatakan_b9" class="d-none">
                                            <label class="mt-2">Sila nyatakan</label>
                                            <input type="text" class="form-control" name="b9a" />
                                        </div>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>20. Apa pendapat anda tentang aliran tunai positif / kewangan berada dalam keadaan baik dan stabil?</label>
                                        <input type="text" class="form-control" name="b10" />
                                    </div>
                                </div>
                                
                                <div class="d-flex justify-content-end mt-4">
                                    <button class="btn btn-tertiary " type="button" id="btnBackBahagian4">Kembali</button>
                                    <button class="btn btn-tertiary mx-2" type="button" id="btnNextBahagian4">Seterusnya</button>
                                </div>
                            </div>

                            <div class="card-body p-4 d-none" id="tab-5">
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>21. Siapakah guru kewangan yang paling anda minati?</label>
                                        <select class="form-select nyatakan" data-nyatakan="6" name="c1">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">Tiada</option>
                                            <option value="2">Financial Faiz</option>
                                            <option value="3">Dr Adam Zubir</option>
                                            <option value="4">Faizul Ridzuan</option>
                                            <option value="5">Afyan Mat Rawi</option>
                                            <option value="6"> Lain-lain (sila nyatakan)</option>
                                        </select>
                                        <div id="nyatakan_c1" class="d-none">
                                            <label class="mt-2">Sila nyatakan</label>
                                            <input type="text" class="form-control" name="c1a" />
                                        </div>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>22. Bilakah anda selesa untuk dihubungi bagi sesi perbualan lanjut?</label>
                                        <select class="form-select" name="c2">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">Pagi</option>
                                            <option value="2">Tengah Hari</option>
                                            <option value="3">Petang</option>
                                            <option value="4">Malam</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="d-flex justify-content-end mt-4">
                                    <button class="btn btn-tertiary " type="button" id="btnBackBahagian5">Kembali</button>
                                    <button class="btn btn-tertiary mx-2" type="button" id="btnNextBahagian5">Hantar Tinjauan Saya</button>
                                </div>
                            </div>


                            <div class="card-body p-4 d-none" id="tab-6">
                                <div class="text-center mt-2">
                                    <h5 class="text-primary">Sila isi butiran akaun bank anda untuk memudahkan kami untuk menghantar suatu ganjaran sebanyak RM200.</h5>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>Nama Penuh (seperti IC)</label>
                                        <input type="text" class="form-control" name="memberFullname" disabled />
                                    </div>
                                </div>
                                <div class="mt-2">
                                    <div class="d-flex flex-column">
                                        <label>Jantina</label>
                                        <select class="form-select" name="gender">
                                            <option value="0">Sila pilih jawapan anda</option>
                                            <option value="1">Lelaki</option>
                                            <option value="2">Perempuan</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-2">
                                    <div class="d-flex flex-column">
                                        <label>Nombor IC</label>
                                        <input type="text" class="form-control" name="icNumber" />
                                    </div>
                                </div>
                                <div class="mt-2">
                                    <div class="d-flex flex-column">
                                        <label>Nama Bank (Pilih bank)</label>
                                        <select class="form-select nyatakan" name="bankId" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>"></select>
                                        <div id="nyatakan_bankId" class="d-none">
                                            <label class="mt-2">Sila nyatakan</label>
                                            <input type="text" class="form-control" name="bankOther" />
                                        </div>
                                    </div>
                                </div>
                                <div class="mt-2">
                                    <div class="d-flex flex-column">
                                        <label>Nombor Akaun Bank</label>
                                        <input type="text" class="form-control" name="bankAccountNumber" />
                                    </div>
                                </div>    
                                <div class="mt-2">
                                    <div class="d-flex flex-column">
                                        <label>Alamat e-mel</label>
                                        <input type="email" class="form-control" name="emailAddress" />
                                    </div>
                                </div>    
                                <div class="d-flex justify-content-end mt-4">
                                    <button class="btn btn-tertiary " type="button" id="btnBackBahagian6">Kembali</button>
                                    <button class="btn btn-tertiary mx-2" type="button" id="btnNextBahagian6">Hantar</button>
                                </div>
                            </div>

                            <div class="card-body p-4 d-none" id="tab-7">
                                <div class="text-center mt-2">
                                    <h5 class="text-primary">Sila Muat Turun dokumen-dokumen untuk pengesahan.</h5>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>IC</label>
                                        <input type="file" class="form-control" name="upldIc" id="upldIc" accept=".png, .jpg, .jpeg, .pdf"/>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>Slip Gaji terkini</label>
                                        <input type="file" class="form-control" name="upldPayslip" id="upldPayslip" accept=".png, .jpg, .jpeg, .pdf"/>
                                    </div>
                                </div>
                                <div class="d-flex justify-content-end mt-4">
                                    <button class="btn btn-tertiary " type="button" id="btnBackBahagian7">Kembali</button>
                                    <button class="btn btn-tertiary mx-2" type="button" id="btnNextBahagian7">Hantar</button>
                                </div>
                            </div>

                             <div class="card-body p-4 d-none" id="tab-8">
                                 <h4>Terima kasih kerana membantu kami memahami situasi kewangan rakyat Malaysia dengan lebih baik. Anda akan menerima ganjaran sebanyak RM200 selepas pengesahan dalam tempoh sekurangnya 7-14 hari bekerja.</h4>
                                 <center>
                                     <a href="<%= Page.ResolveUrl("~/SignIn") %>" class="btn btn-tertiary">Login</a>
                                 </center>
                             </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- footer -->
        <footer class="footer">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="text-center">
                            <p class="mb-0">
                              © Hak Cipta <script>document.write(new Date().getFullYear())</script> © YAYASAN AMANAH BANTUAN AWAM MALAYSIA - Hak cipta terpelihara.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </footer>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type='text/javascript' src="<%=Page.ResolveUrl("~/assets/libs/select2/js/select2.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/assets/libs/jqueryValidation/dist/jquery.validate.js") %>"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <script>
        var vm
        var member
        var bagA, bagB1, bagB2
        var form = $('#form1')

        $(".date-input").datepicker({
            format: 'dd-mm-yyyy',
            autoclose: true
        });

        //checkbox terms
        $('[data-group="terms"]').on('change', function (e) {
            let countChecked = $('[data-group="terms"]:checked').length

            if (countChecked == $('[data-group="terms"]').length) {
                $('[name="terms-all"]').prop('checked', true)
            }
            else {
                $('[name="terms-all"]').prop('checked', false)
            }
        })

        //checkbox all terms
        $('[name="terms-all"]').on('change', function () {
            const val = $(this).prop('checked')
            $('[data-group="terms"]').each(function (i, el) {                
                $(el).prop('checked',val)
            })
        })

        $('[multiple="multiple"].no-min-max-answer').select2({ placeholder: 'Sila pilih jawapan anda', theme: 'bootstrap-5'})
        $('[multiple="multiple"]').select2({ placeholder: 'Sila pilih 3 jawapan', theme: 'bootstrap-5'})

        $('.decimal-input').on('input', function () {
            // Get the current value
            let value = $(this).val();

            // Use regex to allow only numbers and one decimal point
            value = value.replace(/[^0-9.]/g, ''); // Remove non-numeric characters

            // Check for multiple decimal points
            const decimalCount = (value.match(/\./g) || []).length;
            if (decimalCount > 1) {
                value = value.replace(/\.+$/, ''); // Remove the last decimal point
            }

            // Check if there are more than 2 decimal places
            const decimalPart = value.split('.')[1];
            if (decimalPart && decimalPart.length > 2) {
                value = value.substring(0, value.indexOf('.') + 3); // Limit to 2 decimal places
            }

            // Update the input value
            $(this).val(value);
        });

        $('.decimal-input-pct').on('input', function () {
            // Get the current value
            let value = $(this).val();

            // Use regex to allow only numbers and one decimal point
            value = value.replace(/[^0-9.]/g, ''); // Remove non-numeric characters

            // Check for multiple decimal points
            const decimalCount = (value.match(/\./g) || []).length;
            if (decimalCount > 1) {
                value = value.replace(/\.+$/, ''); // Remove the last decimal point
            }

            // Check if there are more than 2 decimal places
            const decimalPart = value.split('.')[1];
            if (decimalPart && decimalPart.length > 2) {
                value = value.substring(0, value.indexOf('.') + 3); // Limit to 2 decimal places
            }

            // Check for maximum value of 100
            if (parseFloat(value) > 100) {
                value = '100'; // Set to 100 if exceeds
            }

            // Update the input value
            $(this).val(value);
        });

        //$('select').select2({ 'theme': 'bootstrap-5', 'minimumResultsForSearch': -1 });

        //button next
        $('#btnNextBahagian1').on('click', function () {
            form.validate().destroy()
            form.validate({
                rules: {
                    terms: {
                        required: true,
                    },
                    'terms-yabam': {
                        required: true,
                    },
                    'privacy-policy': {
                        required: true,
                    },
                    'cookies-policy': {
                        required: true,
                    },
                },
                messages: {
                    terms: {
                        required: "Anda mesti bersetuju dengan terma dan syarat"
                    },
                    'terms-yabam': {
                        required: "Anda mesti bersetuju dengan terma yabam"
                    },
                    'privacy-policy': {
                        required: "Anda mesti bersetuju dengan dasar privasi"
                    },
                    'cookies-policy': {
                        required: "Anda mesti bersetuju dengan dasar kuki"
                    }
                },
                errorClass: 'is-invalid',
                errorPlacement: function (error, element) {
                    error.addClass("invalid-feedback")
                    if (element.is('[data-error-target]')) {
                        error.appendTo(element.data('error-target'))
                    }
                    else {
                        error.insertAfter(element)
                    }
                }
            })
            if (form.valid()) {
                goTab(1, 2)
            }
        })

        $('#btnNextBahagian2').on('click', async function () {

            //force login member
            if (vm.LoginMember != null) {
                setAfterMemberFilled()
                goTab(2, 3)
                return
            }

            form.validate().destroy()
            form.validate({
                rules: {
                    accountFullname: {
                        required: true,
                    },
                    accountPhone: {
                        required: true,
                    },
                },
                errorClass: 'is-invalid',
                errorPlacement: function (error, element) {
                    error.addClass("invalid-feedback");
                    error.insertAfter(element);
                }
            })
            if (form.valid()) {
                $(this).prop('disabled', true)


                try {
                    const res = await ApiHelper.post(window.location.origin + '/SurveyCtrl/RegisterMemberBySurvey', {
                        FullName: $('[name="accountFullname"]').val(),
                        PhoneNumber: $('[name="accountPhone"]').val(),
                    })
                    if (!res.data.Error) {
                        member = res.data.ObjVal
                        setAfterMemberFilled()
                        goTab(2, 3)
                    } else {
                        dialogHelper.error(res.data.Message)
                    }
                } catch (e) {
                    dialogHelper.error(e.message)
                }

                $(this).prop('disabled', false)
            }
            else {
                dialogHelper.error('Sila isikan medan yang diperlukan.')
            }

        })

        $('#btnNextBahagian3').on('click', function () {           
            form.validate().destroy()
            form.validate({
                rules: {
                    a1: {
                        notZero: true,
                    },
                    a2: {
                        notZero: true,
                    },
                    a2a: {
                        required: {
                            depends: function (e) {
                                return $('[name="a2"]').val().includes(String($('[name="a2"]').data('nyatakan')))
                            }
                        },
                    },
                    a2b: {
                        notZero: true,
                    },
                    a2c: {
                        required: {
                            depends: function (e) {
                                return $('[name="a2b"]').val().includes(String($('[name="a2b"]').data('nyatakan')))
                            }
                        },
                    },
                    a3: {
                        notZero: true,
                    },
                    a4: {
                        notZero: true,
                    },
                    a5: {
                        required: true,
                    },
                    a6: {
                        notZero: true,
                    },
                    a7: {
                        notZero: true,
                    },
                    a8: {
                        notZero: true,
                    },
                    a9: {
                        notZero: true,
                    },
                    a10: {
                        notZero: true,
                    },
                    a10a: {
                        required: {
                            depends: function (e) {
                                return $('[name="a10"]').val().includes(String($('[name="a10"]').data('nyatakan')))
                            }
                        },
                    },
                    employmentStatus: {
                        notZero: true,
                    },
                    employerName: {
                        required: true,
                    },
                    employmentStatusOther: {
                        required: {
                            depends: function (e) {
                                return $('[name="employmentStatus"]').val().includes(String($('[name="employmentStatus"]').data('nyatakan')))
                            }
                        }
                    },
                    yearOfService: {
                        notZero: true
                    },
                    retirementAge: {
                        required: true,
                    },
                    employerType: {
                        notZero: true,
                    },
                    employerTypeOther: {
                        required: {
                            depends: function (e) {
                                return $('[name="employerType"]').val().includes(String($('[name="employerType"]').data('nyatakan')))
                            }
                        }
                    },
                },
                errorClass: 'is-invalid',
                errorPlacement: function (error, element) {
                    error.addClass("invalid-feedback");
                    error.insertAfter(element);
                }
            })
            if (form.valid()) {
                bagA = {
                    a1: $('[name="a1"]').val(),
                    a2: $('[name="a2"]').val(),
                    a2a: $('[name="a2a"]').val(),
                    a2b: $('[name="a2b"]').val(),
                    a2c: $('[name="a2c"]').val(),
                    a3: $('[name="a3"]').val(),
                    a4: $('[name="a4"]').val(),
                    a5: $('[name="a5"]').val(),
                    a6: $('[name="a6"]').val(),
                    a7: $('[name="a7"]').val(),
                    a8: $('[name="a8"]').val(),
                    a9: $('[name="a9"]').val(),
                    a10: $('[name="a10"]').val(),
                    a10a: $('[name="a10a"]').val(),
                }
                goTab(3, 4)
                $('html, body').animate({ scrollTop: 0 }, 200);
            }
            else {
                dialogHelper.error('Sila isikan medan yang diperlukan.')
            }
        })

        $('#btnNextBahagian4').on('click', function () {
            form.validate().destroy()
            form.validate({
                rules: {
                    b1: {
                        notZero: true,
                    },
                    b2: {
                        notZero: true,
                    },
                    b3: {
                        notZero: true,
                    },
                    b4: {
                        notZero: true,
                        multipleMinMax: true,
                    },
                    b5: {
                        notZero: true,
                        multipleMinMax: true,
                    },
                    b5a: {
                        required: {
                            depends: function (e) {
                                return $('[name="b5"]').val().includes(String($('[name="b5"]').data('nyatakan')))
                            }
                        },
                    },
                    b6: {
                        notZero: true,
                    },                   
                    b7: {
                        notZero: true,
                    },
                    b8: {
                        notZero: true,
                        multipleMinMax: true,
                    },
                    b8a: {
                        required: {
                            depends: function (e) {
                                return $('[name="b8"]').val().includes(String($('[name="b8"]').data('nyatakan')))
                            }
                        },
                    },
                    b9: {
                        notZero: true,
                        multipleMinMax: true,
                    },
                    b9a: {
                        required: {
                            depends: function (e) {
                                return $('[name="b9"]').val().includes(String($('[name="b9"]').data('nyatakan')))
                            }
                        },
                    },
                    b10: {
                        required: true,
                    },
                },
                errorClass: 'is-invalid',
                errorPlacement: function (error, element) {
                    error.addClass("invalid-feedback");
                    error.insertAfter(element);
                }
            })
            if (form.valid()) {
                bagB = {
                    b1: $('[name="b1"]').val(),
                    b2: $('[name="b2"]').val(),
                    b3: $('[name="b3"]').val(),
                    b4: $('[name="b4"]').val(),
                    b5: $('[name="b5"]').val(),
                    b5a: $('[name="b5a"]').val(),
                    b6: $('[name="b6"]').val(),
                    b7: $('[name="b7"]').val(),
                    b8: $('[name="b8"]').val(),
                    b8a: $('[name="b8a"]').val(),
                    b9: $('[name="b9"]').val(),
                    b9a: $('[name="b9a"]').val(),
                    b10: $('[name="b10"]').val(),
                }
                goTab(4, 5)
            }
            else {
                dialogHelper.error('Sila isikan medan yang diperlukan.')
            }
        })

        $('#btnNextBahagian5').on('click', function () {
            form.validate().destroy()
            form.validate({
                rules: {
                    c1: {
                        notZero: true,
                    },                    
                    c1a: {
                        required: {
                            depends: function (e) {
                                return $('[name="c1"]').val() == $('[name="c1"]').data('nyatakan')
                            }
                        },
                    },
                    c2: {
                        notZero: true,
                    },
                },
                errorClass: 'is-invalid',
                errorPlacement: function (error, element) {
                    error.addClass("invalid-feedback");
                    error.insertAfter(element);
                }
            })
            if (form.valid()) {
                bagC = {
                    c1: $('[name="c1"]').val(),
                    c1a: $('[name="c1a"]').val(),
                    c2: $('[name="c2"]').val(),
                }
                goTab(5, 6)
            }
            else {
                dialogHelper.error('Sila isikan medan yang diperlukan.')
            }

        })

        $('#btnNextBahagian6').on('click', function () {
            form.validate().destroy()
            form.validate({
                rules: {
                    icNumber: {
                        required: true,
                    },                    
                    gender: {
                        notZero: true
                    },
                    bankId: {
                        notZero: true
                    },
                    bankAccountNumber: {
                        required: true,
                    },
                    emailAddress: {
                        email: true
                    }
                },
                errorClass: 'is-invalid',
                errorPlacement: function (error, element) {
                    error.addClass("invalid-feedback");
                    error.insertAfter(element);
                }
            })
            if (form.valid()) {
                goTab(6, 7)
            }
            else {
                dialogHelper.error('Sila isikan medan yang diperlukan.')
            }

        })

         $('#btnNextBahagian7').on('click', async function () {
            form.validate().destroy()
            form.validate({
                rules: {
                    upldIc: {
                        required: false,
                    },                    
                    upldPayslip: {
                        required: false
                    },                   
                },
                errorClass: 'is-invalid',
                errorPlacement: function (error, element) {
                    error.addClass("invalid-feedback");
                    error.insertAfter(element);
                }
            })
             if (form.valid()) {
                 $(this).prop('disabled', true)

                 try {
                    const formData = new FormData()
                    formData.append('IcFile', $('#upldIc')[0].files[0])
                    formData.append('PayslipFile', $('#upldPayslip')[0].files[0])
                    const data = {
                        ReferralMemberId: vm.ReferralMember != null ? vm.ReferralMember.Value : null,
                        MemberId: member.MemberId,
                        SurveyJson: JSON.stringify({
                            bagA,
                            bagB,
                            bagC,
                        }),
                        ICNumber: $('[name="icNumber"]').val(),
                        BankId: $('[name="bankId"]').val(),
                        BankOther: $('[name="bankOther"]').val(),
                        BankAccountNumber: $('[name="bankAccountNumber"]').val(),
                        StateId: bagA.a1,
                        EmployerTypeId: $('[name="employerType"]').val(),
                        EmployerTypeOther: $('[name="employerTypeOther"]').val(),
                        EmploymentStatusId: $('[name="employmentStatus"]').val(),
                        EmploymentStatusOther: $('[name="employmentStatusOther"]').val(),
                        RetirementAge: $('[name="retirementAge"]').val(),
                        YearOfService: $('[name="yearOfService"]').val(),
                        ReferrerName: $('[name="accountReferralName"]').val(),
                        CompanySectorId: bagA.a2 === 'null' ? null : bagA.a2,
                        CompanySectorOther: bagA.a2a,
                        CompanyDepartmentId: bagA.a2b === 'null' ? null : bagA.a2b,
                        CompanyDepartmentOther: bagA.a2c,
                        CompanyEmployerName: $('[name="employerName"]').val(),
                        GenderId: $('[name="gender"]').val(),
                        Email: $('[name="emailAddress"]').val(),
                     }
                    formData.append('Json', JSON.stringify(data))
                    const res = await ApiHelper.post(window.location.origin + '/SurveyCtrl/CompleteSurvey1Async', formData)
                     if (!res.data.Error) {
                        goTab(7,8)
                    } else {
                        dialogHelper.errorHTML(res.data.Message)
                    }
                } catch (e) {
                    dialogHelper.error(e.message)
                }

                 $(this).prop('disabled', false)
            }
            else {
                dialogHelper.error('Sila isikan medan yang diperlukan.')
            }

         })

        function setAfterMemberFilled() {                        
            $('[name="memberFullname"]').val(member.FullName)
        }

        //button back
        $('#btnBackBahagian2').on('click', function () {
            goTab(2, 1)
        })

        $('#btnBackBahagian3').on('click', function () {
            form.validate().resetForm()
            goTab(3, 2)
        })

        $('#btnBackBahagian4').on('click', function () {
            form.validate().resetForm()
            goTab(4, 3)
        })

        $('#btnBackBahagian5').on('click', function () {
            form.validate().resetForm()
            goTab(5, 4)
        })

        $('#btnBackBahagian6').on('click', function () {
            form.validate().resetForm()
            goTab(6, 5)
        })

        $('#btnBackBahagian7').on('click', function () {
            form.validate().resetForm()
            goTab(7, 6)
        })
        //end button back

        $('#cbCopyFatherAddress').on('change', function () {
            if ($(this).is(':checked')) {
                $('[name="familyMotherAddress"]').val($('[name="familyFatherAddress"]').val())
            }
            else {
                $('[name="familyMotherAddress"]').val('')
            }
        })

        $.validator.addMethod("multipleMinMax", function (value, element) {
            return $(element).data('min') - 1 < value.length && $(element).data('max') > value.length - 1
        },"Panjang tidak sah.")

        $.validator.addMethod("notZero", function (value, element) {
            if ($(element).data('select2') !== undefined) {
                return value.length > 0;
            }
            return this.optional(element) || value !== "0";
        }, "Sila pilih pilihan yang sah.");

        $.validator.addMethod("passwordsMatch", function (value, element) {
            return value === $("#loginPassword").val();
        }, "Passwords do not match.");

        $('.nyatakan').on('change', function (e) {
            $('#nyatakan_' + $(this).attr('name')).addClass('d-none')
            let showNyatakan = false
            const dataNyatakan = String($(this).data('nyatakan'))
            if ($(this).data('select2') !== undefined) {
                showNyatakan = $(this).val().includes(dataNyatakan)
            }
            else {
                showNyatakan = dataNyatakan == $(this).val()
            }
            if (showNyatakan) {
                $('#nyatakan_' + $(this).attr('name')).removeClass('d-none')
            }
        })

        function goTab(currentTab, nextTab) {
            $('#tab-' + currentTab).addClass('d-none')
            $('#tab-' + nextTab).removeClass('d-none')
        }

        $('.decimal-input-pct').on('input', function () {
            // Get the current value
            let value = $(this).val();

            // Use regex to allow only numbers and one decimal point
            value = value.replace(/[^0-9.]/g, ''); // Remove non-numeric characters

            // Check for multiple decimal points
            const decimalCount = (value.match(/\./g) || []).length;
            if (decimalCount > 1) {
                value = value.replace(/\.+$/, ''); // Remove the last decimal point
            }

            // Check if there are more than 2 decimal places
            const decimalPart = value.split('.')[1];
            if (decimalPart && decimalPart.length > 2) {
                value = value.substring(0, value.indexOf('.') + 3); // Limit to 2 decimal places
            }

            // Check for maximum value of 100
            if (parseFloat(value) > 100) {
                value = '100'; // Set to 100 if exceeds
            }

            // Update the input value
            $(this).val(value);
        });

        function prepare() {
            vm.VtBanks.forEach(function (item) {
                $('[name="bankId"]').append(`<option value='${item.Key}'>${item.Text}</option>`)
            })

            vm.States.splice(0,0,{StateId:0,Name:'Sila pilih jawapan anda',CountryId:0})
            vm.States.forEach(function (item) {
                $('[name="a1"]').append(`<option value='${item.StateId}'>${item.Name}</option>`)
            })
            vm.Sectors.splice(0, 0, { Key: 0, Text: 'Sila pilih jawapan anda'})
            vm.Sectors.forEach(function (item) {
                $('[name="a2"]').append(`<option value='${item.Key}'>${item.Text}</option>`)
            })
            $('[name="a2"]').append(`<option value='<%= csa.Library.Constant.OTHER_NUMBER %>'>Lain-lain</option>`)


            //vm.Sectors.forEach(function (item) {
            //    $('[name="sector"]').append(`<option value='${item.Key}'>${item.Text}</option>`)
            //})
            //$('[name="sector"]').append(`<option value='null'>Lain-lain</option>`)

            $('[name="a2"]').trigger('change')
        }

        $('[name="a2"]').on('change', function () {
            $('[name="a2b"]').empty()
            $('[name="a2b"]').append(`<option value='0'>Sila pilih jawapan anda</option>`)
            vm.JobPositions.filter(x=>x.RefValue == $(this).val()).forEach(function (item) {
                $('[name="a2b"]').append(`<option value='${item.Value}'>${item.Text}</option>`)
            })
            if ($(this).val() != 0) {
                $('[name="a2b"]').append(`<option value='<%= csa.Library.Constant.OTHER_NUMBER %>'>Lain-lain</option>`)
            }
        })

        $(document).ready(function () {
            vm = JSON.parse($('#<%= hfModelView.ClientID %>').val())
            prepare()
            if (vm.LoginMember != null) {//disabled input for login member
                $('[name="accountPhone"]').val(vm.LoginMember.PhoneNumber).attr('disabled', true)
                $('[name="accountFullname"]').val(vm.LoginMember.FullName).attr('disabled', true)
            }
            if (vm.ReferralMember != null) {
                $('[name="accountReferralName"]').val(vm.ReferralMember.Text).attr('disabled',true)
            }
            if (vm.SessionAccountSurvey != null) {
                member = vm.SessionAccountSurvey
                setAfterMemberFilled()
                //goTab(1, 3)
            }

            $.extend($.validator.messages, {
                required: "Medan ini diperlukan." // Change default "required" message
            });
        })
    </script>
</asp:Content>