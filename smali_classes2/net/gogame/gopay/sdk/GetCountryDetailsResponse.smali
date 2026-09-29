.class public Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;
.super Ljava/lang/Object;


# instance fields
.field private final a:Ljava/lang/String;

.field private final b:Ljava/util/List;

.field private final c:Ljava/util/Map;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/util/List;Ljava/util/Map;)V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iput-object p1, p0, Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;->a:Ljava/lang/String;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;->b:Ljava/util/List;

    iput-object p3, p0, Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;->c:Ljava/util/Map;

    return-void
.end method


# virtual methods
.method public getBaseUrls()Ljava/util/Map;
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;->c:Ljava/util/Map;

    return-object v0
.end method

.method public getCountries()Ljava/util/List;
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;->b:Ljava/util/List;

    return-object v0
.end method

.method public getCountry()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;->a:Ljava/lang/String;

    return-object v0
.end method
