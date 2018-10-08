package com.google.android.gms.internal;

import org.apache.commons.lang3.time.DateUtils;

public final class zzcbe {
    private static zzcbf<Boolean> zzinl = zzcbf.zzb("measurement.service_enabled", true, true);
    private static zzcbf<Boolean> zzinm = zzcbf.zzb("measurement.service_client_enabled", true, true);
    private static zzcbf<Boolean> zzinn = zzcbf.zzb("measurement.log_third_party_store_events_enabled", false, false);
    private static zzcbf<Boolean> zzino = zzcbf.zzb("measurement.log_installs_enabled", false, false);
    private static zzcbf<Boolean> zzinp = zzcbf.zzb("measurement.log_upgrades_enabled", false, false);
    private static zzcbf<Boolean> zzinq = zzcbf.zzb("measurement.log_androidId_enabled", false, false);
    public static zzcbf<Boolean> zzinr = zzcbf.zzb("measurement.upload_dsid_enabled", false, false);
    public static zzcbf<String> zzins = zzcbf.zzi("measurement.log_tag", "FA", "FA-SVC");
    public static zzcbf<Long> zzint = zzcbf.zzb("measurement.ad_id_cache_time", 10000, 10000);
    public static zzcbf<Long> zzinu = zzcbf.zzb("measurement.monitoring.sample_period_millis", (long) DateUtils.MILLIS_PER_DAY, (long) DateUtils.MILLIS_PER_DAY);
    public static zzcbf<Long> zzinv = zzcbf.zzb("measurement.config.cache_time", (long) DateUtils.MILLIS_PER_DAY, (long) DateUtils.MILLIS_PER_HOUR);
    public static zzcbf<String> zzinw = zzcbf.zzi("measurement.config.url_scheme", "https", "https");
    public static zzcbf<String> zzinx = zzcbf.zzi("measurement.config.url_authority", "app-measurement.com", "app-measurement.com");
    public static zzcbf<Integer> zziny = zzcbf.zzm("measurement.upload.max_bundles", 100, 100);
    public static zzcbf<Integer> zzinz = zzcbf.zzm("measurement.upload.max_batch_size", 65536, 65536);
    public static zzcbf<Integer> zzioa = zzcbf.zzm("measurement.upload.max_bundle_size", 65536, 65536);
    public static zzcbf<Integer> zziob = zzcbf.zzm("measurement.upload.max_events_per_bundle", 1000, 1000);
    public static zzcbf<Integer> zzioc = zzcbf.zzm("measurement.upload.max_events_per_day", 100000, 100000);
    public static zzcbf<Integer> zziod = zzcbf.zzm("measurement.upload.max_error_events_per_day", 1000, 1000);
    public static zzcbf<Integer> zzioe = zzcbf.zzm("measurement.upload.max_public_events_per_day", 50000, 50000);
    public static zzcbf<Integer> zziof = zzcbf.zzm("measurement.upload.max_conversions_per_day", 500, 500);
    public static zzcbf<Integer> zziog = zzcbf.zzm("measurement.upload.max_realtime_events_per_day", 10, 10);
    public static zzcbf<Integer> zzioh = zzcbf.zzm("measurement.store.max_stored_events_per_app", 100000, 100000);
    public static zzcbf<String> zzioi = zzcbf.zzi("measurement.upload.url", "https://app-measurement.com/a", "https://app-measurement.com/a");
    public static zzcbf<Long> zzioj = zzcbf.zzb("measurement.upload.backoff_period", 43200000, 43200000);
    public static zzcbf<Long> zziok = zzcbf.zzb("measurement.upload.window_interval", (long) DateUtils.MILLIS_PER_HOUR, (long) DateUtils.MILLIS_PER_HOUR);
    public static zzcbf<Long> zziol = zzcbf.zzb("measurement.upload.interval", (long) DateUtils.MILLIS_PER_HOUR, (long) DateUtils.MILLIS_PER_HOUR);
    public static zzcbf<Long> zziom = zzcbf.zzb("measurement.upload.realtime_upload_interval", 10000, 10000);
    public static zzcbf<Long> zzion = zzcbf.zzb("measurement.upload.debug_upload_interval", 1000, 1000);
    public static zzcbf<Long> zzioo = zzcbf.zzb("measurement.upload.minimum_delay", 500, 500);
    public static zzcbf<Long> zziop = zzcbf.zzb("measurement.alarm_manager.minimum_interval", 60000, 60000);
    public static zzcbf<Long> zzioq = zzcbf.zzb("measurement.upload.stale_data_deletion_interval", (long) DateUtils.MILLIS_PER_DAY, (long) DateUtils.MILLIS_PER_DAY);
    public static zzcbf<Long> zzior = zzcbf.zzb("measurement.upload.refresh_blacklisted_config_interval", 604800000, 604800000);
    public static zzcbf<Long> zzios = zzcbf.zzb("measurement.upload.initial_upload_delay_time", 15000, 15000);
    public static zzcbf<Long> zziot = zzcbf.zzb("measurement.upload.retry_time", 1800000, 1800000);
    public static zzcbf<Integer> zziou = zzcbf.zzm("measurement.upload.retry_count", 6, 6);
    public static zzcbf<Long> zziov = zzcbf.zzb("measurement.upload.max_queue_time", 2419200000L, 2419200000L);
    public static zzcbf<Integer> zziow = zzcbf.zzm("measurement.lifetimevalue.max_currency_tracked", 4, 4);
    public static zzcbf<Integer> zziox = zzcbf.zzm("measurement.audience.filter_result_max_count", 200, 200);
    public static zzcbf<Long> zzioy = zzcbf.zzb("measurement.service_client.idle_disconnect_millis", 5000, 5000);
}
