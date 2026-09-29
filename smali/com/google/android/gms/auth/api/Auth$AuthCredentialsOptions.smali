.class public final Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/google/android/gms/common/api/Api$ApiOptions$Optional;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/android/gms/auth/api/Auth;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "AuthCredentialsOptions"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions$Builder;
    }
.end annotation


# static fields
.field private static zzdyl:Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions;


# instance fields
.field private final zzdym:Ljava/lang/String;

.field private final zzdyn:Lcom/google/android/gms/auth/api/credentials/PasswordSpecification;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    new-instance v0, Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions$Builder;

    invoke-direct {v0}, Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions$Builder;-><init>()V

    new-instance v1, Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions;

    invoke-direct {v1, v0}, Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions;-><init>(Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions$Builder;)V

    sput-object v1, Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions;->zzdyl:Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions;

    return-void
.end method

.method private constructor <init>(Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions$Builder;)V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    iput-object v0, p0, Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions;->zzdym:Ljava/lang/String;

    invoke-static {p1}, Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions$Builder;->zza(Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions$Builder;)Lcom/google/android/gms/auth/api/credentials/PasswordSpecification;

    move-result-object p1

    iput-object p1, p0, Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions;->zzdyn:Lcom/google/android/gms/auth/api/credentials/PasswordSpecification;

    return-void
.end method


# virtual methods
.method public final zzzs()Landroid/os/Bundle;
    .locals 3

    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "consumer_package"

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "password_specification"

    iget-object v2, p0, Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions;->zzdyn:Lcom/google/android/gms/auth/api/credentials/PasswordSpecification;

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    return-object v0
.end method

.method public final zzzv()Lcom/google/android/gms/auth/api/credentials/PasswordSpecification;
    .locals 1

    iget-object v0, p0, Lcom/google/android/gms/auth/api/Auth$AuthCredentialsOptions;->zzdyn:Lcom/google/android/gms/auth/api/credentials/PasswordSpecification;

    return-object v0
.end method
