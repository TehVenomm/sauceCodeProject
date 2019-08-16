package com.google.android.gms.measurement.internal;

import android.content.Context;
import android.support.annotation.Nullable;
import com.google.android.gms.common.GoogleApiAvailabilityLight;
import com.google.android.gms.common.util.VisibleForTesting;
import com.google.android.gms.internal.measurement.zzca;
import com.google.android.gms.internal.measurement.zzcn;
import com.google.android.gms.internal.measurement.zzct;
import com.google.android.gms.internal.measurement.zzjn;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import org.apache.commons.lang3.time.DateUtils;

@VisibleForTesting
public final class zzak {
    static zzr zzfv;
    /* access modifiers changed from: private */
    public static List<zzdu<?>> zzfw = Collections.synchronizedList(new ArrayList());
    private static Set<zzdu<?>> zzfx = Collections.synchronizedSet(new HashSet());
    private static final zzct zzfy = new zzct(zzcn.zzdh("com.google.android.gms.measurement"));
    @VisibleForTesting
    private static Boolean zzfz;
    private static zzdu<Boolean> zzga;
    private static zzdu<Boolean> zzgb;
    private static zzdu<Boolean> zzgc;
    public static zzdu<Boolean> zzgd;
    public static zzdu<Boolean> zzge;
    public static zzdu<String> zzgf = zza("measurement.log_tag", "FA", "FA-SVC", zzce.zzji);
    public static zzdu<Long> zzgg;
    public static zzdu<Long> zzgh;
    public static zzdu<Long> zzgi = zza("measurement.config.cache_time", Long.valueOf(DateUtils.MILLIS_PER_DAY), Long.valueOf(DateUtils.MILLIS_PER_HOUR), zzdn.zzji);
    public static zzdu<String> zzgj = zza("measurement.config.url_scheme", "https", "https", zzdt.zzji);
    public static zzdu<String> zzgk = zza("measurement.config.url_authority", "app-measurement.com", "app-measurement.com", zzap.zzji);
    public static zzdu<Integer> zzgl;
    public static zzdu<Integer> zzgm;
    public static zzdu<Integer> zzgn;
    public static zzdu<Integer> zzgo;
    public static zzdu<Integer> zzgp;
    public static zzdu<Integer> zzgq;
    public static zzdu<Integer> zzgr;
    public static zzdu<Integer> zzgs;
    public static zzdu<Integer> zzgt;
    public static zzdu<Integer> zzgu;
    public static zzdu<String> zzgv = zza("measurement.upload.url", "https://app-measurement.com/a", "https://app-measurement.com/a", zzbb.zzji);
    public static zzdu<Long> zzgw;
    public static zzdu<Long> zzgx;
    public static zzdu<Long> zzgy;
    public static zzdu<Long> zzgz;
    public static zzdu<Long> zzha;
    public static zzdu<Long> zzhb;
    public static zzdu<Long> zzhc;
    public static zzdu<Long> zzhd;
    public static zzdu<Long> zzhe;
    public static zzdu<Long> zzhf;
    public static zzdu<Long> zzhg;
    public static zzdu<Integer> zzhh;
    public static zzdu<Long> zzhi;
    public static zzdu<Integer> zzhj;
    public static zzdu<Integer> zzhk;
    public static zzdu<Long> zzhl;
    public static zzdu<Boolean> zzhm;
    public static zzdu<String> zzhn = zza("measurement.test.string_flag", "---", "---", zzbs.zzji);
    public static zzdu<Long> zzho;
    public static zzdu<Integer> zzhp;
    public static zzdu<Double> zzhq;
    public static zzdu<Integer> zzhr;
    public static zzdu<Boolean> zzhs;
    public static zzdu<Boolean> zzht;
    public static zzdu<Boolean> zzhu;
    public static zzdu<Boolean> zzhv;
    public static zzdu<Boolean> zzhw;
    public static zzdu<Boolean> zzhx;
    public static zzdu<Boolean> zzhy;
    public static zzdu<Boolean> zzhz;
    public static zzdu<Boolean> zzia;
    public static zzdu<Boolean> zzib;
    public static zzdu<Boolean> zzic;
    public static zzdu<Boolean> zzid;
    public static zzdu<Boolean> zzie;
    public static zzdu<Boolean> zzif;
    public static zzdu<Boolean> zzig;
    public static zzdu<Boolean> zzih;
    public static zzdu<Boolean> zzii;
    public static zzdu<Boolean> zzij;
    public static zzdu<Boolean> zzik;
    public static zzdu<Boolean> zzil;
    public static zzdu<Boolean> zzim;
    public static zzdu<Boolean> zzin;
    public static zzdu<Boolean> zzio;
    public static zzdu<Boolean> zzip;
    public static zzdu<Boolean> zziq;
    private static zzdu<Boolean> zzir;
    public static zzdu<Boolean> zzis;
    public static zzdu<Boolean> zzit;
    public static zzdu<Boolean> zziu;
    public static zzdu<Boolean> zziv;
    public static zzdu<Boolean> zziw;
    public static zzdu<Boolean> zzix;
    public static zzdu<Boolean> zziy;
    public static zzdu<Boolean> zziz;
    private static volatile zzfj zzj;
    public static zzdu<Boolean> zzja;
    public static zzdu<Boolean> zzjb;
    public static zzdu<Boolean> zzjc;
    public static zzdu<Boolean> zzjd;
    public static zzdu<Boolean> zzje;
    private static zzdu<Boolean> zzjf;
    public static zzdu<Boolean> zzjg;
    public static zzdu<Boolean> zzjh;

    static {
        Boolean valueOf = Boolean.valueOf(false);
        zzga = zza("measurement.log_third_party_store_events_enabled", valueOf, valueOf, zzan.zzji);
        Boolean valueOf2 = Boolean.valueOf(false);
        zzgb = zza("measurement.log_installs_enabled", valueOf2, valueOf2, zzam.zzji);
        Boolean valueOf3 = Boolean.valueOf(false);
        zzgc = zza("measurement.log_upgrades_enabled", valueOf3, valueOf3, zzaz.zzji);
        Boolean valueOf4 = Boolean.valueOf(false);
        zzgd = zza("measurement.log_androidId_enabled", valueOf4, valueOf4, zzbi.zzji);
        Boolean valueOf5 = Boolean.valueOf(false);
        zzge = zza("measurement.upload_dsid_enabled", valueOf5, valueOf5, zzbv.zzji);
        Long valueOf6 = Long.valueOf(10000);
        zzgg = zza("measurement.ad_id_cache_time", valueOf6, valueOf6, zzcr.zzji);
        Long valueOf7 = Long.valueOf(DateUtils.MILLIS_PER_DAY);
        zzgh = zza("measurement.monitoring.sample_period_millis", valueOf7, valueOf7, zzda.zzji);
        Integer valueOf8 = Integer.valueOf(100);
        zzgl = zza("measurement.upload.max_bundles", valueOf8, valueOf8, zzao.zzji);
        Integer valueOf9 = Integer.valueOf(65536);
        zzgm = zza("measurement.upload.max_batch_size", valueOf9, valueOf9, zzar.zzji);
        Integer valueOf10 = Integer.valueOf(65536);
        zzgn = zza("measurement.upload.max_bundle_size", valueOf10, valueOf10, zzaq.zzji);
        Integer valueOf11 = Integer.valueOf(1000);
        zzgo = zza("measurement.upload.max_events_per_bundle", valueOf11, valueOf11, zzat.zzji);
        Integer valueOf12 = Integer.valueOf(100000);
        zzgp = zza("measurement.upload.max_events_per_day", valueOf12, valueOf12, zzas.zzji);
        Integer valueOf13 = Integer.valueOf(1000);
        zzgq = zza("measurement.upload.max_error_events_per_day", valueOf13, valueOf13, zzav.zzji);
        Integer valueOf14 = Integer.valueOf(50000);
        zzgr = zza("measurement.upload.max_public_events_per_day", valueOf14, valueOf14, zzau.zzji);
        Integer valueOf15 = Integer.valueOf(500);
        zzgs = zza("measurement.upload.max_conversions_per_day", valueOf15, valueOf15, zzax.zzji);
        Integer valueOf16 = Integer.valueOf(10);
        zzgt = zza("measurement.upload.max_realtime_events_per_day", valueOf16, valueOf16, zzaw.zzji);
        Integer valueOf17 = Integer.valueOf(100000);
        zzgu = zza("measurement.store.max_stored_events_per_app", valueOf17, valueOf17, zzay.zzji);
        Long valueOf18 = Long.valueOf(43200000);
        zzgw = zza("measurement.upload.backoff_period", valueOf18, valueOf18, zzba.zzji);
        Long valueOf19 = Long.valueOf(DateUtils.MILLIS_PER_HOUR);
        zzgx = zza("measurement.upload.window_interval", valueOf19, valueOf19, zzbd.zzji);
        Long valueOf20 = Long.valueOf(DateUtils.MILLIS_PER_HOUR);
        zzgy = zza("measurement.upload.interval", valueOf20, valueOf20, zzbc.zzji);
        Long valueOf21 = Long.valueOf(10000);
        zzgz = zza("measurement.upload.realtime_upload_interval", valueOf21, valueOf21, zzbf.zzji);
        Long valueOf22 = Long.valueOf(1000);
        zzha = zza("measurement.upload.debug_upload_interval", valueOf22, valueOf22, zzbe.zzji);
        Long valueOf23 = Long.valueOf(500);
        zzhb = zza("measurement.upload.minimum_delay", valueOf23, valueOf23, zzbh.zzji);
        Long valueOf24 = Long.valueOf(60000);
        zzhc = zza("measurement.alarm_manager.minimum_interval", valueOf24, valueOf24, zzbg.zzji);
        Long valueOf25 = Long.valueOf(DateUtils.MILLIS_PER_DAY);
        zzhd = zza("measurement.upload.stale_data_deletion_interval", valueOf25, valueOf25, zzbj.zzji);
        Long valueOf26 = Long.valueOf(604800000);
        zzhe = zza("measurement.upload.refresh_blacklisted_config_interval", valueOf26, valueOf26, zzbl.zzji);
        Long valueOf27 = Long.valueOf(15000);
        zzhf = zza("measurement.upload.initial_upload_delay_time", valueOf27, valueOf27, zzbk.zzji);
        Long valueOf28 = Long.valueOf(1800000);
        zzhg = zza("measurement.upload.retry_time", valueOf28, valueOf28, zzbn.zzji);
        Integer valueOf29 = Integer.valueOf(6);
        zzhh = zza("measurement.upload.retry_count", valueOf29, valueOf29, zzbm.zzji);
        Long valueOf30 = Long.valueOf(2419200000L);
        zzhi = zza("measurement.upload.max_queue_time", valueOf30, valueOf30, zzbp.zzji);
        Integer valueOf31 = Integer.valueOf(4);
        zzhj = zza("measurement.lifetimevalue.max_currency_tracked", valueOf31, valueOf31, zzbo.zzji);
        Integer valueOf32 = Integer.valueOf(200);
        zzhk = zza("measurement.audience.filter_result_max_count", valueOf32, valueOf32, zzbr.zzji);
        Long valueOf33 = Long.valueOf(5000);
        zzhl = zza("measurement.service_client.idle_disconnect_millis", valueOf33, valueOf33, zzbq.zzji);
        Boolean valueOf34 = Boolean.valueOf(false);
        zzhm = zza("measurement.test.boolean_flag", valueOf34, valueOf34, zzbt.zzji);
        Long valueOf35 = Long.valueOf(-1);
        zzho = zza("measurement.test.long_flag", valueOf35, valueOf35, zzbu.zzji);
        Integer valueOf36 = Integer.valueOf(-2);
        zzhp = zza("measurement.test.int_flag", valueOf36, valueOf36, zzbx.zzji);
        Double valueOf37 = Double.valueOf(-3.0d);
        zzhq = zza("measurement.test.double_flag", valueOf37, valueOf37, zzbw.zzji);
        Integer valueOf38 = Integer.valueOf(50);
        zzhr = zza("measurement.experiment.max_ids", valueOf38, valueOf38, zzbz.zzji);
        Boolean valueOf39 = Boolean.valueOf(false);
        zzhs = zza("measurement.validation.internal_limits_internal_event_params", valueOf39, valueOf39, zzby.zzji);
        Boolean valueOf40 = Boolean.valueOf(true);
        zzht = zza("measurement.audience.dynamic_filters", valueOf40, valueOf40, zzcb.zzji);
        Boolean valueOf41 = Boolean.valueOf(false);
        zzhu = zza("measurement.reset_analytics.persist_time", valueOf41, valueOf41, zzca.zzji);
        Boolean valueOf42 = Boolean.valueOf(true);
        zzhv = zza("measurement.validation.value_and_currency_params", valueOf42, valueOf42, zzcd.zzji);
        Boolean valueOf43 = Boolean.valueOf(false);
        zzhw = zza("measurement.sampling.time_zone_offset_enabled", valueOf43, valueOf43, zzcc.zzji);
        Boolean valueOf44 = Boolean.valueOf(false);
        zzhx = zza("measurement.referrer.enable_logging_install_referrer_cmp_from_apk", valueOf44, valueOf44, zzcf.zzji);
        Boolean valueOf45 = Boolean.valueOf(true);
        zzhy = zza("measurement.fetch_config_with_admob_app_id", valueOf45, valueOf45, zzch.zzji);
        Boolean valueOf46 = Boolean.valueOf(false);
        zzhz = zza("measurement.client.sessions.session_id_enabled", valueOf46, valueOf46, zzcg.zzji);
        Boolean valueOf47 = Boolean.valueOf(false);
        zzia = zza("measurement.service.sessions.session_number_enabled", valueOf47, valueOf47, zzcj.zzji);
        Boolean valueOf48 = Boolean.valueOf(false);
        zzib = zza("measurement.client.sessions.immediate_start_enabled_foreground", valueOf48, valueOf48, zzci.zzji);
        Boolean valueOf49 = Boolean.valueOf(false);
        zzic = zza("measurement.client.sessions.background_sessions_enabled", valueOf49, valueOf49, zzcl.zzji);
        Boolean valueOf50 = Boolean.valueOf(false);
        zzid = zza("measurement.client.sessions.remove_expired_session_properties_enabled", valueOf50, valueOf50, zzck.zzji);
        Boolean valueOf51 = Boolean.valueOf(false);
        zzie = zza("measurement.service.sessions.session_number_backfill_enabled", valueOf51, valueOf51, zzcn.zzji);
        Boolean valueOf52 = Boolean.valueOf(false);
        zzif = zza("measurement.service.sessions.remove_disabled_session_number", valueOf52, valueOf52, zzcm.zzji);
        Boolean valueOf53 = Boolean.valueOf(true);
        zzig = zza("measurement.collection.firebase_global_collection_flag_enabled", valueOf53, valueOf53, zzcp.zzji);
        Boolean valueOf54 = Boolean.valueOf(false);
        zzih = zza("measurement.collection.efficient_engagement_reporting_enabled", valueOf54, valueOf54, zzco.zzji);
        Boolean valueOf55 = Boolean.valueOf(false);
        zzii = zza("measurement.collection.redundant_engagement_removal_enabled", valueOf55, valueOf55, zzcq.zzji);
        Boolean valueOf56 = Boolean.valueOf(true);
        zzij = zza("measurement.personalized_ads_signals_collection_enabled", valueOf56, valueOf56, zzct.zzji);
        Boolean valueOf57 = Boolean.valueOf(true);
        zzik = zza("measurement.personalized_ads_property_translation_enabled", valueOf57, valueOf57, zzcs.zzji);
        Boolean valueOf58 = Boolean.valueOf(true);
        zzil = zza("measurement.collection.init_params_control_enabled", valueOf58, valueOf58, zzcv.zzji);
        Boolean valueOf59 = Boolean.valueOf(true);
        zzim = zza("measurement.upload.disable_is_uploader", valueOf59, valueOf59, zzcu.zzji);
        Boolean valueOf60 = Boolean.valueOf(true);
        zzin = zza("measurement.experiment.enable_experiment_reporting", valueOf60, valueOf60, zzcx.zzji);
        Boolean valueOf61 = Boolean.valueOf(true);
        zzio = zza("measurement.collection.log_event_and_bundle_v2", valueOf61, valueOf61, zzcw.zzji);
        Boolean valueOf62 = Boolean.valueOf(true);
        zzip = zza("measurement.collection.null_empty_event_name_fix", valueOf62, valueOf62, zzcz.zzji);
        Boolean valueOf63 = Boolean.valueOf(false);
        zziq = zza("measurement.audience.sequence_filters", valueOf63, valueOf63, zzcy.zzji);
        Boolean valueOf64 = Boolean.valueOf(false);
        zzir = zza("measurement.audience.sequence_filters_bundle_timestamp", valueOf64, valueOf64, zzdb.zzji);
        Boolean valueOf65 = Boolean.valueOf(false);
        zzis = zza("measurement.quality.checksum", valueOf65, valueOf65, null);
        Boolean valueOf66 = Boolean.valueOf(true);
        zzit = zza("measurement.module.collection.conditionally_omit_admob_app_id", valueOf66, valueOf66, zzdd.zzji);
        Boolean valueOf67 = Boolean.valueOf(false);
        zziu = zza("measurement.sdk.dynamite.use_dynamite2", valueOf67, valueOf67, zzdc.zzji);
        Boolean valueOf68 = Boolean.valueOf(false);
        zziv = zza("measurement.sdk.dynamite.allow_remote_dynamite", valueOf68, valueOf68, zzdf.zzji);
        Boolean valueOf69 = Boolean.valueOf(false);
        zziw = zza("measurement.sdk.collection.validate_param_names_alphabetical", valueOf69, valueOf69, zzde.zzji);
        Boolean valueOf70 = Boolean.valueOf(false);
        zzix = zza("measurement.collection.event_safelist", valueOf70, valueOf70, zzdh.zzji);
        Boolean valueOf71 = Boolean.valueOf(false);
        zziy = zza("measurement.service.audience.scoped_filters_v27", valueOf71, valueOf71, zzdg.zzji);
        Boolean valueOf72 = Boolean.valueOf(false);
        zziz = zza("measurement.service.audience.session_scoped_event_aggregates", valueOf72, valueOf72, zzdj.zzji);
        Boolean valueOf73 = Boolean.valueOf(false);
        zzja = zza("measurement.service.audience.session_scoped_user_engagement", valueOf73, valueOf73, zzdi.zzji);
        Boolean valueOf74 = Boolean.valueOf(false);
        zzjb = zza("measurement.service.audience.remove_disabled_session_scoped_user_engagement", valueOf74, valueOf74, zzdl.zzji);
        Boolean valueOf75 = Boolean.valueOf(false);
        zzjc = zza("measurement.sdk.collection.retrieve_deeplink_from_bow", valueOf75, valueOf75, zzdk.zzji);
        Boolean valueOf76 = Boolean.valueOf(false);
        zzjd = zza("measurement.app_launch.event_ordering_fix", valueOf76, valueOf76, zzdm.zzji);
        Boolean valueOf77 = Boolean.valueOf(false);
        zzje = zza("measurement.sdk.collection.last_deep_link_referrer", valueOf77, valueOf77, zzdp.zzji);
        Boolean valueOf78 = Boolean.valueOf(false);
        zzjf = zza("measurement.sdk.collection.last_deep_link_referrer_campaign", valueOf78, valueOf78, zzdo.zzji);
        Boolean valueOf79 = Boolean.valueOf(false);
        zzjg = zza("measurement.sdk.collection.last_gclid_from_referrer", valueOf79, valueOf79, zzdr.zzji);
        Boolean valueOf80 = Boolean.valueOf(false);
        zzjh = zza("measurement.upload.file_lock_state_check", valueOf80, valueOf80, zzdq.zzji);
    }

    private static boolean isPackageSide() {
        if (zzfv != null) {
            zzr zzr = zzfv;
        }
        return false;
    }

    @VisibleForTesting
    private static <V> zzdu<V> zza(@Nullable String str, @Nullable V v, @Nullable V v2, @Nullable zzdv<V> zzdv) {
        zzdu<V> zzdu = new zzdu<>(str, v, v2, zzdv);
        zzfw.add(zzdu);
        return zzdu;
    }

    static void zza(zzfj zzfj) {
        zzj = zzfj;
    }

    static void zza(zzr zzr) {
        zzfv = zzr;
    }

    @VisibleForTesting
    static void zza(Exception exc) {
        if (zzj != null) {
            Context context = zzj.getContext();
            if (zzfz == null) {
                zzfz = Boolean.valueOf(GoogleApiAvailabilityLight.getInstance().isGooglePlayServicesAvailable(context, 12451000) == 0);
            }
            if (zzfz.booleanValue()) {
                zzj.zzab().zzgk().zza("Got Exception on PhenotypeFlag.get on Play device", exc);
            }
        }
    }

    static final /* synthetic */ Long zzfu() {
        return Long.valueOf(isPackageSide() ? zzjn.zzyc() : zzjn.zzxo());
    }

    static final /* synthetic */ String zzfx() {
        return isPackageSide() ? zzjn.zzye() : zzjn.zzxp();
    }

    public static Map<String, String> zzk(Context context) {
        zzca zza = zzca.zza(context.getContentResolver(), zzcn.zzdh("com.google.android.gms.measurement"));
        return zza == null ? Collections.emptyMap() : zza.zzre();
    }
}
