<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurveyYabam.ascx.cs" Inherits="csa.Member.Uc.SurveyYabam" %>
<asp:HiddenField ID="hfModelView" runat="server" />
<div class="page-content">
        <div class="container-fluid">
            <!-- page header -->
            <div class="row mb-3 pb-1">
                <div class="col-12">
                    <div class="d-flex align-items-lg-center flex-lg-row flex-column">
                        <div class="flex-grow-1">
                            <h4 class="fs-16 mb-1">YABAM</h4>
                        </div>
                        <div class="mt-3 mt-lg-0">
                            <div class="form">
                                <div class="row g-3 mb-0 align-items-center">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <!-- left panel -->
                <div class="col-12 ">
                    <div class="card">
                        <div class="card-body">

                            <ul class="nav nav-tabs mb-3" role="tablist">
                                <li class="nav-item" role="presentation">
                                    <a class="nav-link active" data-bs-toggle="tab" href="#bagA" role="tab" aria-selected="false">
                                        Bahagian A
                                    </a>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <a class="nav-link" data-bs-toggle="tab" href="#bagB" role="tab" aria-selected="false">
                                        Bahagian B
                                    </a>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <a class="nav-link" data-bs-toggle="tab" href="#bagC" role="tab" aria-selected="false">
                                        Bahagian C
                                    </a>
                                </li>
                            </ul>

                            <div class="tab-content  ">
                                <div class="tab-pane active" id="bagA" role="tabpanel">
                                    <div class="mt-2">
                                    <div class="d-flex flex-column">
                                        <label>1. Negeri manakah yang anda tinggal sekarang?</label>
                                        <select class="form-select no-border readonly-select" name="a1"></select>
                                    </div>
                                </div>
                                  <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>2. Apakah jawatan/ posisi anda di tempat kerja?</label>
                                        <div class="row">
                                            <div class="col-6 col-md-3">
                                                <select class="form-select nyatakan no-border readonly-select" name="a2" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>"></select>
                                                <div id="nyatakan_a2" class="d-none">
                                                    <label class="mt-2">Sila nyatakan jawatan anda</label>
                                                    <input type="text" class="form-control no-border" name="a2a" readonly/>
                                                </div>
                                            </div>
                                            <div class="col-6 col-md-5">
                                                <select class="form-select nyatakan col-3 no-border readonly-select" name="a2b" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>"></select>
                                                <div id="nyatakan_a2b" class="d-none">
                                                    <label class="mt-2">Sila nyatakan posisi anda</label>
                                                    <input type="text" class="form-control no-border" name="a2c" readonly/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>3. Apakah Taraf Pendidikan Anda?</label>
                                        <select class="form-select no-border readonly-select" name="a3">
                                            <option value="0">Select an option</option>
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
                                        <select class="form-select no-border readonly-select" name="a4">
                                            <option value="0">Select an option</option>
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
                                        <input type="number" class="form-control no-border" name="a5" readonly/>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>6. Adakah anda mempunyai tanggungan ahli keluarga OKU atau sakit tenat?</label>
                                        <select class="form-select no-border readonly-select" name="a6">
                                            <option value="0">Select an option</option>
                                            <option value="1">Ya</option>
                                            <option value="2">Tidak</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>7. Adakah anda mempunyai rumah sendiri atau menyewa?</label>
                                        <select class="form-select no-border readonly-select" name="a7">
                                            <option value="0">Select an option</option>
                                            <option value="1">Memiliki rumah sendiri</option>
                                            <option value="2">Menyewa</option>
                                            <option value="3">Rumah ibu bapa</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>8. Dimanakah lokasi kediaman rumah anda?</label>
                                        <select class="form-select no-border readonly-select" name="a8">
                                            <option value="0">Select an option</option>
                                            <option value="1">Dalam Bandar</option>
                                            <option value="2">Luar Bandar</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>9. Berapakah jumlah kerata yang anda miliki?</label>
                                        <select class="form-select no-border readonly-select" name="a9">
                                            <option value="0">Select an option</option>
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
                                        <select class="form-select nyatakan" name="a10" data-nyatakan="16" multiple="multiple" required disabled>
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
                                            <input type="text" class="form-control no-border" name="a10a" readonly/>
                                        </div>
                                    </div>
                                </div> 
                                </div>
                                <div class="tab-pane" id="bagB" role="tabpanel">
                                   <div class="mt-2">
                                    <div class="d-flex flex-column">
                                        <label>11. Berapakah jumlah pendapatan kasar sekeluarga anda?</label>
                                        <select class="form-select no-border readonly-select" name="b1">
                                            <option value="0">Select an option</option>
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
                                        <select class="form-select no-border readonly-select" name="b2">
                                            <option value="0">Select an option</option>
                                            <option value="1">< RM100,000</option>
                                            <option value="2">RM100,001 - RM200,000</option>
                                            <option value="3">> RM200,000</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>13. Berapakah simpanan anda sebulan?</label>
                                        <select class="form-select no-border readonly-select" name="b3">
                                            <option value="0">Select an option</option>
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
                                        <select class="form-select" name="b4" multiple="multiple" data-min="3" data-max="3" disabled>
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
                                        <select class="form-select nyatakan" name="b5" data-nyatakan="10" multiple="multiple" data-min="3" data-max="3" disabled>
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
                                            <input type="text" class="form-control no-border" name="b5a" readonly/>
                                        </div>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>16. Sepanjang tahun terkini, berapa kerap anda terpaksa meminjam wang atau menggunakan kredit untuk menampung perbelanjaan harian?</label>
                                        <select class="form-select no-border readonly-select" name="b6">
                                            <option value="0">Select an option</option>
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
                                        <select class="form-select no-border readonly-select" name="b7">
                                            <option value="0">Select an option</option>
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
                                        <select class="form-select nyatakan" name="b8" data-nyatakan="13" multiple="multiple" data-min="3" data-max="3" disabled>
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
                                        <select class="form-select nyatakan" name="b9" data-nyatakan="10" multiple="multiple" data-min="3" data-max="3" disabled>
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
                                        <input type="text" class="form-control no-border" name="b10" readonly/>
                                    </div>
                                </div>
                            </div>

                                <div class="tab-pane" id="bagC" role="tabpanel">
                                   <div class="mt-2">
                                    <div class="d-flex flex-column">
                                        <label>21. Siapakah guru kewangan yang paling anda minati?</label>
                                        <select class="form-select nyatakan no-border readonly-select" data-nyatakan="6" name="c1">
                                            <option value="0">Select an option</option>
                                            <option value="1">Tiada</option>
                                            <option value="2">Financial Faiz</option>
                                            <option value="3">Dr Adam Zubir</option>
                                            <option value="4">Faizul Ridzuan</option>
                                            <option value="5">Afyan Mat Rawi</option>
                                            <option value="6"> Lain-lain (sila nyatakan)</option>
                                        </select>
                                        <div id="nyatakan_c1" class="d-none">
                                            <label class="mt-2">Sila nyatakan</label>
                                            <input type="text" class="form-control no-border" name="c1a" readonly/>
                                        </div>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <div class="d-flex flex-column">
                                        <label>22. Bilakah anda selesa untuk dihubungi bagi sesi perbualan lanjut?</label>
                                        <select class="form-select no-border readonly-select" name="c2">
                                            <option value="0">Select an option</option>
                                            <option value="1">Pagi</option>
                                            <option value="2">Tengah Hari</option>
                                            <option value="3">Petang</option>
                                            <option value="4">Malam</option>
                                        </select>
                                    </div>
                                </div>
                            </div>

                            
                        </div>
                        </div>
                    </div>
                </div>
            </div>
    </div>

<script>
   

    function loadYabam() {
        if ($('#<%= hfModelView.ClientID %>').val() == '') {
            return
        }
        vm = JSON.parse($('#<%= hfModelView.ClientID %>').val())
        var ans = JSON.parse(vm.Answer)

        vm.States.splice(0, 0, { StateId: 0, Name: 'Select an option', CountryId: 0 })
        vm.States.forEach(function (item) {
            $('[name="a1"]').append(`<option value='${item.StateId}'>${item.Name}</option>`)
        })

        vm.Sectors.splice(0, 0, { Key: 0, Text: 'Select an option' })
        vm.Sectors.forEach(function (item) {
            $('[name="a2"]').append(`<option value='${item.Key}'>${item.Text}</option>`)
        })
        $('[name="a2"]').append(`<option value='<%= csa.Library.Constant.OTHER_NUMBER %>'>Lain-lain</option>`)

        $('[name="a2"]').on('change', function () {
            $('[name="a2b"]').empty()
            $('[name="a2b"]').append(`<option value='0'>Select an option</option>`)
            vm.JobPositions.filter(x => x.RefValue == $(this).val()).forEach(function (item) {
                $('[name="a2b"]').append(`<option value='${item.Value}'>${item.Text}</option>`)
            })
            if ($(this).val() != 0) {
                $('[name="a2b"]').append(`<option value='<%= csa.Library.Constant.OTHER_NUMBER %>'>Lain-lain</option>`)
            }
        })

        //bahagian A
        $('[name="a1"]').val(ans.bagA.a1).trigger('change')
        $('[name="a2"]').val(ans.bagA.a2).trigger('change')
        $('[name="a2a"]').val(ans.bagA.a2a)
        $('[name="a2b"]').val(ans.bagA.a2b).trigger('change')
        $('[name="a2c"]').val(ans.bagA.a2c)
        $('[name="a3"]').val(ans.bagA.a3).trigger('change')
        $('[name="a4"]').val(ans.bagA.a4).trigger('change')
        $('[name="a5"]').val(ans.bagA.a5)
        $('[name="a6"]').val(ans.bagA.a6).trigger('change')
        $('[name="a7"]').val(ans.bagA.a7).trigger('change')
        $('[name="a8"]').val(ans.bagA.a8).trigger('change')
        $('[name="a9"]').val(ans.bagA.a9).trigger('change')
        $('[name="a10"]').val(ans.bagA.a10).trigger('change')
        $('[name="a10a"]').val(ans.bagA.a10a)


        //bahagian B
        $('[name="b1"]').val(ans.bagB.b1).trigger('change')
        $('[name="b2"]').val(ans.bagB.b2).trigger('change')
        $('[name="b3"]').val(ans.bagB.b3).trigger('change')
        $('[name="b4"]').val(ans.bagB.b4).trigger('change')
        $('[name="b5"]').val(ans.bagB.b5).trigger('change')
        $('[name="b5a"]').val(ans.bagB.b5a)
        $('[name="b6"]').val(ans.bagB.b6).trigger('change')
        $('[name="b7"]').val(ans.bagB.b7).trigger('change')
        $('[name="b8"]').val(ans.bagB.b8).trigger('change')
        $('[name="b8a"]').val(ans.bagB.b8a)
        $('[name="b9"]').val(ans.bagB.b9).trigger('change')
        $('[name="b9a"]').val(ans.bagB.b9a)
        $('[name="b10"]').val(ans.bagB.b10)

        //bahagian C
        $('[name="c1"]').val(ans.bagC.c1).trigger('change')
        $('[name="c1a"]').val(ans.bagC.c1a)
        $('[name="c2"]').val(ans.bagC.c2).trigger('change')
    }
</script>