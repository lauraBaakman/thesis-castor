namespace Registration {

    public class Settings {

        private IPointSelector _pointSelector;

        public IPointSelector PointSelector {
            get {
                return _pointSelector;
            }

            set {
                _pointSelector = value;
            }
        }

        public Settings()
        {
            PointSelector = new SelectAllPointsSelector();
        }
    }
}

