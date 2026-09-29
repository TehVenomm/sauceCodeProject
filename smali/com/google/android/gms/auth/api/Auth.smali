.class public final Lcom/google/android/gms/auth/api/Auth;
.super Ljava/lang/Object;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions;
    }
.end annotation


# static fields
.field public static final CREDENTIALS_API:Lcom/google/android/gms/common/api/Api;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/Api<",
            "Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions;",
            ">;"
        }
    .end annotation
.end field

.field public static final CredentialsApi:Lcom/google/android/gms/auth/api/credentials/CredentialsApi;

.field public static final GOOGLE_SIGN_IN_API:Lcom/google/android/gms/common/api/Api;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/Api<",
            "Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions;",
            ">;"
        }
    .end annotation
.end field

.field public static final GoogleSignInApi:Lcom/google/android/gms/auth/api/signin/GoogleSignInApi;

.field public static final PROXY_API:Lcom/google/android/gms/common/api/Api;
    .annotation build Lcom/google/android/gms/common/annotation/KeepForSdk;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/Api<",
            "Lcom/google/android/gms/auth/api/zzf;",
            ">;"
        }
    .end annotation
.end field

.field public static final ProxyApi:Lcom/google/android/gms/auth/api/proxy/ProxyApi;
    .annotation build Lcom/google/android/gms/common/annotation/KeepForSdk;
    .end annotation
.end field

.field public static final zzdyd:Lcom/google/android/gms/common/api/Api$zzf;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/Api$zzf<",
            "Lcom/google/android/gms/internal/zzaso;",
            ">;"
        }
    .end annotation
.end field

.field private static zzdye:Lcom/google/android/gms/common/api/Api$zzf;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/Api$zzf<",
            "Lcom/google/android/gms/internal/zzark;",
            ">;"
        }
    .end annotation
.end field

.field public static final zzdyf:Lcom/google/android/gms/common/api/Api$zzf;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/Api$zzf<",
            "Lcom/google/android/gms/auth/api/signin/internal/zzd;",
            ">;"
        }
    .end annotation
.end field

.field private static final zzdyg:Lcom/google/android/gms/common/api/Api$zza;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/Api$zza<",
            "Lcom/google/android/gms/internal/zzaso;",
            "Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions;",
            ">;"
        }
    .end annotation
.end field

.field private static final zzdyh:Lcom/google/android/gms/common/api/Api$zza;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/Api$zza<",
            "Lcom/google/android/gms/internal/zzark;",
            "Lcom/google/android/gms/common/api/Api$ApiOptions$NoOptions;",
            ">;"
        }
    .end annotation
.end field

.field private static final zzdyi:Lcom/google/android/gms/common/api/Api$zza;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/Api$zza<",
            "Lcom/google/android/gms/auth/api/signin/internal/zzd;",
            "Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions;",
            ">;"
        }
    .end annotation
.end field

.field private static zzdyj:Lcom/google/android/gms/common/api/Api;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/Api<",
            "Lcom/google/android/gms/common/api/Api$ApiOptions$NoOptions;",
            ">;"
        }
    .end annotation
.end field

.field private static zzdyk:Lcom/google/android/gms/internal/zzari;


# direct methods
.method static constructor <clinit>()V
    .locals 4

    new-instance v0, Lcom/google/android/gms/common/api/Api$zzf;

    invoke-direct {v0}, Lcom/google/android/gms/common/api/Api$zzf;-><init>()V

    sput-object v0, Lcom/google/android/gms/auth/api/Auth;->zzdyd:Lcom/google/android/gms/common/api/Api$zzf;

    new-instance v0, Lcom/google/android/gms/common/api/Api$zzf;

    invoke-direct {v0}, Lcom/google/android/gms/common/api/Api$zzf;-><init>()V

    sput-object v0, Lcom/google/android/gms/auth/api/Auth;->zzdye:Lcom/google/android/gms/common/api/Api$zzf;

    new-instance v0, Lcom/google/android/gms/common/api/Api$zzf;

    invoke-direct {v0}, Lcom/google/android/gms/common/api/Api$zzf;-><init>()V

    sput-object v0, Lcom/google/android/gms/auth/api/Auth;->zzdyf:Lcom/google/android/gms/common/api/Api$zzf;

    new-instance v0, Lcom/google/android/gms/auth/api/zza;

    invoke-direct {v0}, Lcom/google/android/gms/auth/api/zza;-><init>()V

    sput-object v0, Lcom/google/android/gms/auth/api/Auth;->zzdyg:Lcom/google/android/gms/common/api/Api$zza;

    new-instance v0, Lcom/google/android/gms/auth/api/zzb;

    invoke-direct {v0}, Lcom/google/android/gms/auth/api/zzb;-><init>()V

    sput-object v0, Lcom/google/android/gms/auth/api/Auth;->zzdyh:Lcom/google/android/gms/common/api/Api$zza;

    new-instance v0, Lcom/google/android/gms/auth/api/zzc;

    invoke-direct {v0}, Lcom/google/android/gms/auth/api/zzc;-><init>()V

    sput-object v0, Lcom/google/android/gms/auth/api/Auth;->zzdyi:Lcom/google/android/gms/common/api/Api$zza;

    sget-object v0, Lcom/google/android/gms/auth/api/zzd;->API:Lcom/google/android/gms/common/api/Api;

    sput-object v0, Lcom/google/android/gms/auth/api/Auth;->PROXY_API:Lcom/google/android/gms/common/api/Api;

    new-instance v0, Lcom/google/android/gms/common/api/Api;

    const-string v1, "Auth.CREDENTIALS_API"

    sget-object v2, Lcom/google/android/gms/auth/api/Auth;->zzdyg:Lcom/google/android/gms/common/api/Api$zza;

    sget-object v3, Lcom/google/android/gms/auth/api/Auth;->zzdyd:Lcom/google/android/gms/common/api/Api$zzf;

    invoke-direct {v0, v1, v2, v3}, Lcom/google/android/gms/common/api/Api;-><init>(Ljava/lang/String;Lcom/google/android/gms/common/api/Api$zza;Lcom/google/android/gms/common/api/Api$zzf;)V

    sput-object v0, Lcom/google/android/gms/auth/api/Auth;->CREDENTIALS_API:Lcom/google/android/gms/common/api/Api;

    new-instance v0, Lcom/google/android/gms/common/api/Api;

    const-string v1, "Auth.GOOGLE_SIGN_IN_API"

    sget-object v2, Lcom/google/android/gms/auth/api/Auth;->zzdyi:Lcom/google/android/gms/common/api/Api$zza;

    sget-object v3, Lcom/google/android/gms/auth/api/Auth;->zzdyf:Lcom/google/android/gms/common/api/Api$zzf;

    invoke-direct {v0, v1, v2, v3}, Lcom/google/android/gms/common/api/Api;-><init>(Ljava/lang/String;Lcom/google/android/gms/common/api/Api$zza;Lcom/google/android/gms/common/api/Api$zzf;)V

    sput-object v0, Lcom/google/android/gms/auth/api/Auth;->GOOGLE_SIGN_IN_API:Lcom/google/android/gms/common/api/Api;

    new-instance v0, Lcom/google/android/gms/common/api/Api;

    const-string v1, "Auth.ACCOUNT_STATUS_API"

    sget-object v2, Lcom/google/android/gms/auth/api/Auth;->zzdyh:Lcom/google/android/gms/common/api/Api$zza;

    sget-object v3, Lcom/google/android/gms/auth/api/Auth;->zzdye:Lcom/google/android/gms/common/api/Api$zzf;

    invoke-direct {v0, v1, v2, v3}, Lcom/google/android/gms/common/api/Api;-><init>(Ljava/lang/String;Lcom/google/android/gms/common/api/Api$zza;Lcom/google/android/gms/common/api/Api$zzf;)V

    sput-object v0, Lcom/google/android/gms/auth/api/Auth;->zzdyj:Lcom/google/android/gms/common/api/Api;

    new-instance v0, Lcom/google/android/gms/internal/zzatn;

    invoke-direct {v0}, Lcom/google/android/gms/internal/zzatn;-><init>()V

    sput-object v0, Lcom/google/android/gms/auth/api/Auth;->ProxyApi:Lcom/google/android/gms/auth/api/proxy/ProxyApi;

    new-instance v0, Lcom/google/android/gms/internal/zzasg;

    invoke-direct {v0}, Lcom/google/android/gms/internal/zzasg;-><init>()V

    sput-object v0, Lcom/google/android/gms/auth/api/Auth;->CredentialsApi:Lcom/google/android/gms/auth/api/credentials/CredentialsApi;

    new-instance v0, Lcom/google/android/gms/internal/zzarj;

    invoke-direct {v0}, Lcom/google/android/gms/internal/zzarj;-><init>()V

    sput-object v0, Lcom/google/android/gms/auth/api/Auth;->zzdyk:Lcom/google/android/gms/internal/zzari;

    new-instance v0, Lcom/google/android/gms/auth/api/signin/internal/zzc;

    invoke-direct {v0}, Lcom/google/android/gms/auth/api/signin/internal/zzc;-><init>()V

    sput-object v0, Lcom/google/android/gms/auth/api/Auth;->GoogleSignInApi:Lcom/google/android/gms/auth/api/signin/GoogleSignInApi;

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
